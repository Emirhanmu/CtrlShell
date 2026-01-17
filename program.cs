using System;
using Renci.SshNet;
using Renci.SshNet.Common;

class Program
{
    static int Main()
    {
        // Change here
        var host = "192.168.1.10";
        var port = 22;
        var username = "root";
        var password = "passw"; 

        try
        {
            Console.WriteLine($"[INFO] Trying SSH connect to {host}:{port} ...");

            using var client = new SshClient(host, port, username, password);
            client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(8);

            client.Connect();

            if (!client.IsConnected)
            {
                Console.WriteLine("[FAIL] Connect() done but IsConnected=false");
                return 1;
            }

            Console.WriteLine("[OK] SSH connected successfully.");

            var cmd = client.CreateCommand("whoami && hostname");
            var output = cmd.Execute();
            Console.WriteLine("[REMOTE OUTPUT]");
            Console.WriteLine(output);

            client.Disconnect();
            Console.WriteLine("[INFO] Disconnected.");
            return 0;
        }
        catch (SshAuthenticationException)
        {
            Console.WriteLine("[FAIL] Authentication failed (wrong user/pass or key).");
            return 2;
        }
        catch (SshConnectionException ex)
        {
            Console.WriteLine("[FAIL] Connection error: " + ex.Message);
            return 3;
        }
        catch (SshOperationTimeoutException)
        {
            Console.WriteLine("[FAIL] Timeout.");
            return 4;
        }
        catch (Exception ex)
        {
            Console.WriteLine("[FAIL] Unexpected: " + ex.Message);
            return 5;
        }
    }
}
