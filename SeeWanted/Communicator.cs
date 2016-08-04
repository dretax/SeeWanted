using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeeWanted
{
    internal sealed class Communicator
    {
        internal const string Separator = "~";

        internal enum Codes
        {
            Login,
            InvalidLogin,
            IsLeader,
            NotLeader,
            GetUserData,
            GetAllUsers,
            GetAllVehicles,
            GetAllPersons,
            RegisterUser,
            InvalidRegistration,
            OkRegistration,
            DeleteUser,
            InvalidDeletion,
            OkDeletion,
            RegisterVehicle,
            RegisterPerson,
            OkRVehicle,
            OkRPerson,
            DeleteVehicle,
            DeletePerson,
            DeleteFail,
            DeletePersonOk,
            DeleteVehicleOk,
            Version,
            RequestCheck,
            Alert,
            GetAllArchivedV,
            GetAllArchivedP,
            GetBookedPersons,
            DeleteBookedP,
            AddBookedP,
            BookedExists,
            OkRBookedP,
            AddBookedReason,
            FailedToAddBookedR,
            GetBookedPD,
            GetBookedPDData
        }

        internal static async Task<string> SendMessage(string msg)
        {
            try
            {
                using (var tcpClient = new TcpClient())
                {
                    await tcpClient.ConnectAsync("144.76.5.197", 28015);
                    using (var networkStream = tcpClient.GetStream())
                    {
                        UTF8Encoding asen = new UTF8Encoding();
                        msg = Encrypter.EncryptData(msg);
                        byte[] ba = asen.GetBytes(msg);
                        await networkStream.WriteAsync(ba, 0, ba.Length);

                        var buffer = new byte[4096];
                        var byteCount = await networkStream.ReadAsync(buffer, 0, buffer.Length);
                        var message = Encoding.UTF8.GetString(buffer, 0, byteCount);
                        message = Encrypter.DecryptData(message);
                        tcpClient.Close();
                        return message;
                    }
                }

                /*TcpClient tcpclnt = new TcpClient();
                //tcpclnt.Connect("144.76.5.197", 28015);
                //tcpclnt.Connect("127.0.0.1", 28015);
                Stream stm = tcpclnt.GetStream();

                UTF8Encoding asen = new UTF8Encoding();
                msg = Encrypter.EncryptData(msg);
                byte[] ba = asen.GetBytes(msg);

                stm.Write(ba, 0, ba.Length);

                byte[] bb = new byte[8000];
                int k = stm.Read(bb, 0, 8000);
                string message = "";
                message = Encoding.UTF8.GetString(bb, 0, k);
                MessageBox.Show(message.Length.ToString());
                message = Encrypter.DecryptData(message);
                //MessageBox.Show(message);
                tcpclnt.Close();
                //MessageBox.Show(message);
                return message;*/
            }

            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a kommunikáció közben! Ezt fotózd le DreTaXnak: " + ex);
            }
            return null;
        }
    }
}
