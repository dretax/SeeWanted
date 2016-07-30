using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Alert
        }

        internal static string SendMessage(string msg)
        {
            try
            {
                TcpClient tcpclnt = new TcpClient();
                tcpclnt.Connect("144.76.5.197", 28015);
                Stream stm = tcpclnt.GetStream();

                UTF8Encoding asen = new UTF8Encoding();
                byte[] ba = asen.GetBytes(msg);

                stm.Write(ba, 0, ba.Length);

                byte[] bb = new byte[400000];
                int k = stm.Read(bb, 0, 400000);
                string message = "";
                /*for (int i = 0; i < k; i++)
                {
                    message = message + Convert.ToChar(bb[i]);
                }*/
                message = Encoding.UTF8.GetString(bb, 0, k);
                tcpclnt.Close();
                //MessageBox.Show(message);
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
