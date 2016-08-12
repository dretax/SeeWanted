using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
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
            GetBookedPDData,
            GetVehicleData,
            GetPersonData,
            GetAllVehKeys,
            GetAllPersonKeys,
            GetAllArchivedVKeys,
            GetAllArchivedPKeys,
            GetArchivedVKey,
            GetArchivedPKey,
            GetBookedAPD,
            GetBookedAPDData,
            GiveLeader
        }

        internal static string SendMessage(string msg)
        {
            try
            {

                TcpClient tcpclnt = new TcpClient();
                tcpclnt.Connect("144.76.5.197", 28015);
                //tcpclnt.Connect("127.0.0.1", 28015);
                Stream stm = tcpclnt.GetStream();

                UTF8Encoding asen = new UTF8Encoding();
                msg = Encrypter.EncryptData(msg);
                byte[] ba = asen.GetBytes(msg);

                stm.Write(ba, 0, ba.Length);

                byte[] bb = new byte[10000];
                int k = stm.Read(bb, 0, 10000);
                string message = "";
                message = Encoding.UTF8.GetString(bb, 0, k);
                message = Encrypter.DecryptData(message);
                tcpclnt.Close();
                return message;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt a kommunikáció közben! Ezt fotózd le DreTaXnak: " + ex);
            }
            return null;
        }
    }
}
