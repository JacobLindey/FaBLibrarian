using System.Diagnostics;
using FabLibrarian.CardLibrary.Services.FabOS.Model;
using FabLibrarian.CardLibrary.Services.Local.Abstractions;
using FabLibrarian.CardLibrary.Services.Local.Model;

namespace FabLibrarian.CardLibrary.Services.FabOS;

public class FabCardConverterFactory : ILocalCardModelConverter<CardDao>
{
    private readonly IReadOnlyCollection<CardDao> _daos;

    public FabCardConverterFactory(IReadOnlyCollection<CardDao> daos)
    {
        _daos = daos;
    }
    
    private LocalCardModel ToLocalCardModel(IReadOnlyList<CardDao> daos)
    {
        Debug.Assert(daos.Any());
        Debug.Assert(daos.Select(x => x.Name).Distinct().Count() == 1);

        var cardName = daos[0].Name;
        var databaseUrl = $"https://fabdb.net/cards/{daos[0].Printings[0].Id}";
        
        var imageUris = new List<string>();
        foreach (var dao in daos)
        {
            var defaultPrinting = dao.Printings.FirstOrDefault(x => x.ImageUrl is not null);
            if (defaultPrinting?.ImageUrl is null) continue;

            imageUris.Add(defaultPrinting.ImageUrl);

            if (defaultPrinting.DoubleSidedCardInfo is not null && defaultPrinting.DoubleSidedCardInfo.Any())
            {
                var otherSide = defaultPrinting.DoubleSidedCardInfo[0];
                if (otherSide.IsDFC)
                {
                    var otherImage = otherSide.IsFront 
                        ? GetReferencedImage(otherSide.OtherFaceUniqueId)
                        : GetReferencingImage(defaultPrinting.UniqueId);

                    if (otherImage is not null)
                    {
                        imageUris.Add(otherImage);
                    }
                }
            }
        }

        return new LocalCardModel(cardName, databaseUrl, imageUris.ToArray());
    }

    private string? GetReferencedImage(string uniqueId)
    {
        return _daos.FirstOrDefault(x => x.Printings.Any(printing => printing.UniqueId == uniqueId))?.Printings[0].ImageUrl;
    }

    private string? GetReferencingImage(string uniqueId)
    {
        return _daos.FirstOrDefault(
                x => x.Printings.Any(
                    printing =>
                        printing.UniqueId != uniqueId &&
                        printing.DoubleSidedCardInfo is not null && 
                        printing.DoubleSidedCardInfo[0].OtherFaceUniqueId == uniqueId)
            )?
           .Printings[0].ImageUrl;
    }

    public IEnumerable<LocalCardModel> ToLocalCardModels()
    {
        return _daos.GroupBy(x => x.Name)
                    .Select(grouping =>
                         ToLocalCardModel(grouping.ToArray()))
                    .ToList();
    }
}