using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace CourseWorkCodeFirst
{
    public static class ProjectSourcePath
    {
        private const string myRelativePath = nameof(ProjectSourcePath) + ".cs";
        private static string? lazyValue;
        public static string Value => lazyValue ??= calculatePath();

        private static string calculatePath()
        {
            string pathName = GetSourceFilePathName();
            if (!pathName.EndsWith(myRelativePath, StringComparison.Ordinal)) 
            {
                throw new Exception("Unexpected path");
            }
            return pathName.Substring(0, pathName.Length - myRelativePath.Length);
        }

        private static string GetSourceFilePathName([CallerFilePath] string? callerFilePath = null) => callerFilePath ?? "";
    }
}
