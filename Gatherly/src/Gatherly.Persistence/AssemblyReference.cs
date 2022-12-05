using System.Reflection;

namespace Gatherly.Persistence;

public static class AssemblyReference
{
    public static Assembly Assembly => typeof(AssemblyReference).Assembly;
}