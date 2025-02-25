using Microsoft.AspNetCore.Mvc.Testing;
using NotesApp.Auth.FunctionalTests.Base;

namespace NotesApp.Auth.FunctionalTests
{
    internal class TokenTests(
        WebApplicationFactory<Program> factory) : AuthTestBase(factory)
    {

    }
}
