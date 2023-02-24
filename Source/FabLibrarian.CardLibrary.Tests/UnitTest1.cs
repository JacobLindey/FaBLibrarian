using System.Threading.Tasks;
using FabLibrarian.CardLibrary.Model;
using Xunit;

namespace FabLibrarian.CardLibrary.Tests;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        var api = new ScryfallApi();
        var result = await api.NamedSearchAsync(new NamedSearchOptions("aust com"));
    }
}