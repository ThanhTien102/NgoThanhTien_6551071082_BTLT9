using Serilog;

namespace Bai2
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) => HandleException(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => HandleException((Exception)e.ExceptionObject);

            ApplicationConfiguration.Initialize();
            Application.Run(new FormKhachThue());
        }

        private static void HandleException(Exception ex)
        {
            Log.Error(ex, "Đã xảy ra lỗi hệ thống");
            MessageBox.Show("Đã xảy ra lỗi, thông tin đã được ghi lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}