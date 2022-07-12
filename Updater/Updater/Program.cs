using CommandLine;
using Updater.Options;
using Updater.Utils;

namespace Updater
{
    public class Program
    {
        private readonly static int InstallDirReadInterval = 6000;

        public static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<ApplicationUpdaterOptions>(args)
                .MapResult(
                    (ApplicationUpdaterOptions options) => HandleApplicationUpdate(options),
                    errs => Task.FromResult(0)
                    );
        }

        private static async Task HandleApplicationUpdate(ApplicationUpdaterOptions options)
        {
            var updater = new Updater(options);

            try
            {
                updater.Update();

                while (true)
                {
                    await Task.Delay(InstallDirReadInterval).ContinueWith(_ =>
                    {
                        updater.Update();
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

