using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace PhonePad.Editor.Build
{
    public static class BuildAndroid
    {
        private const string DefaultOutputPath = "Builds/Android/Phone2Pad.apk";

        public static void Build()
        {
            var outputPath = GetArg("-outputPath") ?? DefaultOutputPath;
            outputPath = outputPath.Replace('\\', '/');

            var outputDirectory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrWhiteSpace(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            var versionName = GetArg("-versionName");
            if (!string.IsNullOrWhiteSpace(versionName))
            {
                PlayerSettings.bundleVersion = versionName;
            }

            var versionCodeStr = GetArg("-versionCode");
            if (!string.IsNullOrWhiteSpace(versionCodeStr) && int.TryParse(versionCodeStr, out var versionCode))
            {
                PlayerSettings.Android.bundleVersionCode = versionCode;
            }

            EditorUserBuildSettings.buildAppBundle = false;

            var scenes = EditorBuildSettings.scenes
                .Where(scene => scene.enabled)
                .Select(scene => scene.path)
                .ToArray();

            if (scenes.Length == 0)
            {
                throw new InvalidOperationException("No enabled scenes found in ProjectSettings/EditorBuildSettings.asset.");
            }

            var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
            {
                scenes = scenes,
                locationPathName = outputPath,
                target = BuildTarget.Android,
                options = BuildOptions.None,
            });

            if (report.summary.result != BuildResult.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Android build failed: {report.summary.result} ({report.summary.totalErrors} errors).");
            }
        }

        private static string? GetArg(string name)
        {
            var args = Environment.GetCommandLineArgs();
            var index = Array.IndexOf(args, name);
            if (index < 0 || index >= args.Length - 1)
            {
                return null;
            }

            return args[index + 1];
        }
    }
}
