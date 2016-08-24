using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SeeWantedServer
{
    internal sealed class Program
    {
        private static Thread _t;
        internal static TcpListener myList;
        internal static UTF8Encoding asen = new UTF8Encoding();
        internal const string Separator = "~";
        internal static string Pathh;
        internal const string Version = "1.7";
        private static bool Run = true;

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

        internal static void Main(string[] args)
        {
            try
            {
                Pathh = Directory.GetCurrentDirectory();
                IPAddress ipAd = IPAddress.Any;
                myList = new TcpListener(ipAd
                    , 28015);
                myList.Start();
                Logger.Log("SeeWanted Server Created By DreTaX & Gartand");
                Logger.Log("The server is running at port 28015...");
                Logger.Log("The local End point is: " +
                                  myList.LocalEndpoint);
                Logger.Log("Waiting for a connection.....");
                _t = new Thread(new ThreadStart(ThreadVoid));
                _t.Start();
            }
            catch (Exception e)
            {
                Logger.Log("Error..... " + e.StackTrace);
            }
        }

        internal static void ThreadVoid()
        {
            while (Run)
            {
                Socket s = myList.AcceptSocket();
                try
                {

                    byte[] b = new byte[10000];
                    int k = s.Receive(b);
                    string message = "";
                    message = Encoding.UTF8.GetString(b, 0, k);
                    message = Encrypter.DecryptData(message);
                    string[] split = message.Split(Convert.ToChar("="));
                    Codes code;
                    int intp = -1;
                    int.TryParse(split[0], out intp);
                    if (intp == -1)
                    {
                        Logger.Log("Invalid message. Maybe some1 tryin to shit?");
                        s.Close();
                        continue;
                    }
                    code = (Codes) intp;
                    string[] otherdata = split[1].Split(Convert.ToChar(Separator));
                    //Logger.Log("New Connection " + s.RemoteEndPoint + " Data: " + code + " - " + string.Join(" ", otherdata));
                    var ini = new IniParser(Pathh + "\\DB.ini");
                    string bmsg = "";
                    switch (code)
                    {
                        case Codes.Login:
                        {
                            var pw = ini.GetSetting("Users", otherdata[0].ToLower());
                            if (string.IsNullOrEmpty(pw))
                            {
                                bmsg = Encrypter.EncryptData((int) Codes.InvalidLogin + "=InvalidLogin");
                                s.Send(asen.GetBytes(bmsg));
                                s.Close();
                                continue;
                            }
                            if (pw == otherdata[1])
                            {
                                var leaderdata = ini.GetSetting("Leaders", otherdata[0]);
                                if (leaderdata == null)
                                {
                                    bmsg = Encrypter.EncryptData((int) Codes.NotLeader + "=Not leader");
                                    s.Send(asen.GetBytes(bmsg));
                                    s.Close();
                                    continue;
                                }
                                bmsg = Encrypter.EncryptData((int) Codes.IsLeader + "=" + otherdata[0] + Separator +
                                                          leaderdata);
                                s.Send(asen.GetBytes(bmsg));
                                s.Close();
                            }
                            else
                            {
                                bmsg = Encrypter.EncryptData((int) Codes.InvalidLogin + "=Invalid Login");
                                s.Send(asen.GetBytes(bmsg));
                                s.Close();
                            }
                            continue;
                        }
                        case Codes.IsLeader:
                        {
                            var leaderdata = ini.GetSetting("Leaders", otherdata[0]);
                            if (leaderdata == null)
                            {
                                bmsg = Encrypter.EncryptData((int) Codes.NotLeader + "=Not leader");
                                s.Send(asen.GetBytes(bmsg));
                                s.Close();
                                continue;
                            }
                            bmsg = Encrypter.EncryptData((int) Codes.IsLeader + "=" + otherdata[0] + Separator + leaderdata);
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.GetUserData:
                        {
                            Logger.Log("Adatlekérés: '" + otherdata[0] + "' " + s.RemoteEndPoint);
                            var userdata = ini.GetSetting("Users", otherdata[0]);
                            if (userdata == null)
                            {
                                bmsg = Encrypter.EncryptData((int)Codes.GetUserData + "=null");
                                s.Send(
                                    asen.GetBytes(bmsg));
                                s.Close();
                                continue;
                            }
                            var clan = ini.GetSetting("UserClan", otherdata[0]);
                            bmsg = Encrypter.EncryptData((int)Codes.GetUserData + "=" + otherdata[0] + Separator + userdata + Separator +
                                                         clan);
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.GetAllUsers:
                        {
                            string alluserswithfaction = ini.EnumSection("Users").Aggregate("",
                                (current, x) => current + x + ">" + ini.GetSetting("UserClan", x) + Separator);
                            alluserswithfaction = alluserswithfaction.Trim(Convert.ToChar(Separator));
                            bmsg = Encrypter.EncryptData((int) Codes.GetAllUsers + "=" + alluserswithfaction);
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.GetAllVehicles:
                        {
                            string allvehiclesn = ini.EnumSection("Cars").Aggregate("", (current, x) => current + x + "$" + ini.GetSetting("Cars", x) + Separator);
                            allvehiclesn = allvehiclesn.Trim(Convert.ToChar("~"));
                            bmsg = Encrypter.EncryptData(((int)Codes.GetAllVehicles) + "=" + allvehiclesn);
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                           continue;
                        }
                        case Codes.GetAllPersons:
                        {
                            string allvehiclesn = ini.EnumSection("Szemelyek").Aggregate("", (current, x) => current + x + "$" + ini.GetSetting("Szemelyek", x) + Separator);
                            bmsg = Encrypter.EncryptData(((int)Codes.GetAllPersons + "=" + allvehiclesn));
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.RegisterUser:
                        {
                            if (ini.GetSetting("Users", otherdata[0]) != null)
                            {
                                bmsg =
                                    Encrypter.EncryptData(((int) Codes.InvalidRegistration + "=" +
                                                           "A Felhasznalo mar letezik!"));
                                s.Send(asen.GetBytes(bmsg));
                                s.Close();
                                continue;
                            }
                            ini.AddSetting("Users", otherdata[0], otherdata[1]);
                            ini.AddSetting("UserClan", otherdata[0], otherdata[2]);
                            ini.Save();
                            bmsg = Encrypter.EncryptData(((int) Codes.OkRegistration + "=" + "Sikeresen regisztraltad!"));
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.DeleteUser:
                        {
                            if (ini.GetSetting("Leaders", otherdata[0]) != null && otherdata[1] != "adm")
                            {
                                bmsg = Encrypter.EncryptData(((int)Codes.InvalidDeletion + "=" + "Nem torolhetsz leadert!"));
                                s.Send(asen.GetBytes(bmsg));
                                s.Close();
                                continue;
                            }
                            var clan = ini.GetSetting("UserClan", otherdata[0]);
                            if (clan != null)
                            {
                                if (clan != otherdata[1] && otherdata[1] != "adm")
                                {
                                    bmsg = Encrypter.EncryptData(((int)Codes.InvalidDeletion + "=" +
                                                                  "Nem torolhetsz masik frakcioba tartozo tagot!"));
                                    s.Send(
                                        asen.GetBytes(bmsg));
                                    s.Close();
                                    continue;
                                }
                                if (clan == "adm" && otherdata[1] == "adm")
                                {
                                    bmsg = Encrypter.EncryptData(((int)Codes.InvalidDeletion + "=" +
                                                                  "Nem torolhetsz adminisztrátort!"));
                                    s.Send(
                                        asen.GetBytes(bmsg));
                                    s.Close();
                                    continue;
                                }
                            }
                            if (ini.GetSetting("Users", otherdata[0]) != null)
                            {
                                ini.DeleteSetting("Users", otherdata[0]);
                            }
                            if (ini.GetSetting("UserClan", otherdata[0]) != null)
                            {
                                ini.DeleteSetting("UserClan", otherdata[0]);
                            }
                            if (ini.GetSetting("Leaders", otherdata[0]) != null)
                            {
                                ini.DeleteSetting("Leaders", otherdata[0]);
                            }
                            ini.Save();
                            bmsg = Encrypter.EncryptData(((int)Codes.OkDeletion + "=" + "Sikeresen toroltel egy felhasznalot!"));
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.RegisterVehicle:
                        {
                            int length = ini.EnumSection("Cars").Length + 1;
                            if (ini.GetSetting("Cars", length.ToString()) != null)
                            {
                                var plus = length + 1;
                                for (int i = plus; i < 100000; i++)
                                {
                                    if (ini.GetSetting("Cars", i.ToString()) == null)
                                    {
                                        length = i;
                                        break;
                                    }
                                }
                            }
                            string ss = otherdata.Aggregate("", (current, x) => current + x);
                            ini.AddSetting("Cars", length.ToString(), ss);
                            ini.Save();
                            bmsg = Encrypter.EncryptData(((int)Codes.OkRVehicle + "=" + "Sikeresen regisztraltal egy jarmure korozest!"));
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.RegisterPerson:
                        {
                            int length = ini.EnumSection("Szemelyek").Length + 1;
                            if (ini.GetSetting("Szemelyek", length.ToString()) != null)
                            {
                                var plus = length + 1;
                                for (int i = plus; i < 100000; i++)
                                {
                                    if (ini.GetSetting("Szemelyek", i.ToString()) == null)
                                    {
                                        length = i;
                                        break;
                                    }
                                }
                            }
                            string ss = otherdata.Aggregate("", (current, x) => current + x);
                            ini.AddSetting("Szemelyek", length.ToString(), ss);
                            ini.Save();
                            bmsg = Encrypter.EncryptData(((int)Codes.OkRPerson + "=" + "Sikeresen kiadtal egy szemelyre korozest!"));
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.DeleteVehicle:
                        {
                            var faction = ini.GetSetting("UserClan", otherdata[1]);
                            var data = ini.GetSetting("Cars", otherdata[0]);
                            var enumm = ini.EnumSection("CarsAR");
                            if (data != null)
                            {
                                ini.DeleteSetting("Cars", otherdata[0]);
                                ini.AddSetting("CarsAR", (enumm.Length + 1).ToString(), data + "<" + otherdata[1] + " - " + faction + " - " + DateTime.Now + "<" + otherdata[2]);
                                ini.Save();
                                bmsg = Encrypter.EncryptData(((int)Codes.DeleteVehicleOk + "=Sikeresen toroltel egy korozest!"));
                                s.Send(asen.GetBytes(bmsg));
                                s.Close();
                                Logger.Log(otherdata[1] + " deleted: " + data);
                                continue;
                            }
                            bmsg = Encrypter.EncryptData((Codes.DeleteFail + "=" + "Korozes nem talalhato! (Lehet mar elodted toroltek)"));
                            s.Send(System.Convert.FromBase64String(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.DeletePerson:
                        {
                            var faction = ini.GetSetting("UserClan", otherdata[1]);
                            var data = ini.GetSetting("Szemelyek", otherdata[0]);
                            var enumm = ini.EnumSection("SzemelyekAR");
                            if (data != null)
                            {
                                ini.DeleteSetting("Szemelyek", otherdata[0]);
                                ini.AddSetting("SzemelyekAR", (enumm.Length + 1).ToString(), data + "<" + otherdata[1] + " - " + faction + " - " + DateTime.Now + "<" + otherdata[2]);
                                ini.Save();
                                bmsg = Encrypter.EncryptData(((int)Codes.DeletePersonOk + "=" + "Sikeresen toroltel egy korozest!"));
                                s.Send(asen.GetBytes(bmsg));
                                s.Close();
                                Logger.Log(otherdata[1] + " deleted: " + data);
                                continue;
                            }
                            bmsg = Encrypter.EncryptData(((int)Codes.DeleteFail + "=" + "Korozes nem talalhato! (Lehet mar elodted toroltek)"));
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.Version:
                        {
                            bmsg = Encrypter.EncryptData(((int) Codes.Version + "=" + Version));
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.GetAllArchivedV:
                        {
                            string allvehiclesn = ini.EnumSection("CarsAR").Aggregate("", (current, x) => current + x + "$" + ini.GetSetting("CarsAR", x) + Separator);
                            allvehiclesn = allvehiclesn.Trim(Convert.ToChar("~"));
                            bmsg = Encrypter.EncryptData(((int)Codes.GetAllArchivedV + "=" + allvehiclesn));
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.GetAllArchivedP:
                        {
                            string allvehiclesn = ini.EnumSection("SzemelyekAR").Aggregate("", (current, x) => current + x + "$" + ini.GetSetting("SzemelyekAR", x) + Separator);
                            allvehiclesn = allvehiclesn.Trim(Convert.ToChar("~"));
                            bmsg = Encrypter.EncryptData(((int)Codes.GetAllArchivedP + "=" + allvehiclesn));
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.AddBookedP:
                        {
                            string name = otherdata[0];
                            string id = otherdata[1];
                            string reason = otherdata[2];
                            if (ini.GetSetting("Nyilvantartasok", name) != null)
                            {
                                bmsg = Encrypter.EncryptData(((int)Codes.BookedExists + "=Ez a felhasználó létezik!"));
                                s.Send(asen.GetBytes(bmsg));
                                s.Close();
                                continue;
                            }
                            ini.AddSetting("Nyilvantartasok", name, id);
                            ini.AddSetting("NyilvantartasokR", name, reason);
                            ini.Save();
                            bmsg = Encrypter.EncryptData(((int)Codes.OkRBookedP + "=Sikeresen regisztrálva!"));
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.DeleteBookedP:
                        {
                            string name = otherdata[0];
                            var data = ini.GetSetting("Nyilvantartasok", name);
                            var data2 = ini.GetSetting("NyilvantartasokR", name);
                            if (data != null)
                            {
                                Logger.Log(otherdata[1] + " - " + otherdata[2] + " deleted: " + name + " - " + data);
                                ini.DeleteSetting("Nyilvantartasok", name);
                                ini.DeleteSetting("NyilvantartasokR", name);
                                ini.AddSetting("NyilvantartasokAR", name, data + "$" + data2 + "$" + otherdata[1] + "$"
                                                                          + otherdata[2] + "$" + DateTime.Now + "$" + otherdata[3]);
                                ini.Save();
                                bmsg = Encrypter.EncryptData(((int)Codes.OkDeletion + "=Törölve!"));
                                s.Send(asen.GetBytes(bmsg));
                                s.Close();
                                continue;
                            }
                            bmsg = Encrypter.EncryptData(((int)Codes.DeleteFail + "=A név nem létezik!"));
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.GetBookedPersons:
                        {
                            string allbookedp = ini.EnumSection("Nyilvantartasok").Aggregate("", 
                                (current, x) => current + x + "$" + ini.GetSetting("Nyilvantartasok", x) + "<");
                            allbookedp = allbookedp.Trim(Convert.ToChar("~"));
                            bmsg = Encrypter.EncryptData(((int) Codes.GetBookedPersons + "=" + allbookedp));
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.AddBookedReason:
                        {
                            string name = otherdata[0];
                            if (ini.GetSetting("NyilvantartasokR", name) != null)
                            {
                                var data = ini.GetSetting("NyilvantartasokR", name);
                                ini.SetSetting("NyilvantartasokR", name, data + otherdata[1] + ";");
                                ini.Save();
                                bmsg = Encrypter.EncryptData(((int)Codes.AddBookedP + "=Sikeres hozzáadás!"));
                                s.Send(asen.GetBytes(bmsg));
                                s.Close();
                                continue;
                            }
                            bmsg = Encrypter.EncryptData(((int)Codes.FailedToAddBookedR + "=Sikertelen!"));
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.GetBookedPD:
                        {
                            string name = otherdata[0];
                            if (ini.GetSetting("Nyilvantartasok", name) != null)
                            {
                                var data = ini.GetSetting("Nyilvantartasok", name);
                                bmsg = Encrypter.EncryptData(((int) Codes.GetBookedPD + "=" + data));
                                s.Send(asen.GetBytes(bmsg));
                                s.Close();
                                continue;
                            }
                            bmsg = Encrypter.EncryptData(((int) Codes.GetBookedPD + "=null"));
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.GetBookedPDData:
                        {
                            string name = otherdata[0];
                            if (ini.GetSetting("NyilvantartasokR", name) != null)
                            {
                                var data = ini.GetSetting("NyilvantartasokR", name);
                                bmsg = Encrypter.EncryptData(((int)Codes.GetBookedPDData + "=" + data));
                                s.Send(asen.GetBytes(bmsg));
                                s.Close();
                                continue;
                            }
                            bmsg = Encrypter.EncryptData(((int)Codes.GetBookedPDData + "=null"));
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.GetAllVehKeys:
                        {
                            string allvehiclesn = ini.EnumSection("Cars").Aggregate("", (current, x) => current + x + "$");
                            allvehiclesn = allvehiclesn.Trim(Convert.ToChar("$"));
                            bmsg = Encrypter.EncryptData(((int)Codes.GetAllVehKeys) + "=" + allvehiclesn);
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.GetAllPersonKeys:
                        {
                            string allvehiclesn = ini.EnumSection("Szemelyek").Aggregate("", (current, x) => current + x + "$");
                            allvehiclesn = allvehiclesn.Trim(Convert.ToChar("$"));
                            bmsg = Encrypter.EncryptData(((int)Codes.GetAllPersonKeys) + "=" + allvehiclesn);
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.GetPersonData:
                        {
                            string name = otherdata[0];
                            var data = ini.GetSetting("Szemelyek", name) ?? "null";
                            bmsg = Encrypter.EncryptData(((int)Codes.GetPersonData) + "=" + data);
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.GetVehicleData:
                        {
                            string name = otherdata[0];
                            var data = ini.GetSetting("Cars", name) ?? "null";
                            bmsg = Encrypter.EncryptData(((int)Codes.GetVehicleData) + "=" + data);
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.GetAllArchivedVKeys:
                        {
                            string allvehiclesn = ini.EnumSection("CarsAR").Aggregate("", (current, x) => current + x + "$");
                            allvehiclesn = allvehiclesn.Trim(Convert.ToChar("$"));
                            bmsg = Encrypter.EncryptData(((int)Codes.GetAllArchivedVKeys) + "=" + allvehiclesn);
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.GetAllArchivedPKeys:
                        {
                            string allvehiclesn = ini.EnumSection("SzemelyekAR").Aggregate("", (current, x) => current + x + "$");
                            allvehiclesn = allvehiclesn.Trim(Convert.ToChar("$"));
                            bmsg = Encrypter.EncryptData(((int)Codes.GetAllArchivedPKeys) + "=" + allvehiclesn);
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.GetArchivedPKey:
                        {
                            string name = otherdata[0];
                            var data = ini.GetSetting("SzemelyekAR", name) ?? "null";
                            bmsg = Encrypter.EncryptData(((int)Codes.GetArchivedPKey) + "=" + data);
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.GetArchivedVKey:
                        {
                            string name = otherdata[0];
                            var data = ini.GetSetting("CarsAR", name) ?? "null";
                            bmsg = Encrypter.EncryptData(((int)Codes.GetArchivedVKey) + "=" + data);
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.GetBookedAPD:
                        {
                            string allvehiclesn = ini.EnumSection("NyilvantartasokAR").Aggregate("", (current, x) => current + x + "<");
                            allvehiclesn = allvehiclesn.Trim(Convert.ToChar("<"));
                            bmsg = Encrypter.EncryptData(((int)Codes.GetBookedAPD) + "=" + allvehiclesn);
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.GetBookedAPDData:
                        {
                            string name = otherdata[0];
                            var data = ini.GetSetting("NyilvantartasokAR", name) ?? "null";
                            bmsg = Encrypter.EncryptData(((int)Codes.GetBookedAPDData) + "=" + data);
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        case Codes.GiveLeader:
                        {
                            if (ini.GetSetting("Leaders", otherdata[0]) == null)
                            {
                                ini.AddSetting("Leaders", otherdata[0], otherdata[1]);
                                ini.Save();
                            }
                            else
                            {
                                ini.SetSetting("Leaders", otherdata[0], otherdata[1]);
                                ini.Save();
                            }
                            bmsg = Encrypter.EncryptData(((int) Codes.GiveLeader) + "=Done");
                            s.Send(asen.GetBytes(bmsg));
                            s.Close();
                            continue;
                        }
                        default:
                        {
                            s.Close();
                            continue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (!ex.ToString().ToLower().Contains("formatexception"))
                    {
                        Logger.Log("Exception: " + ex);
                    }
                    s.Close();
                }
            }
        }
    }
}
