using System.Net.Sockets;//                                                                      для библиотек для работы с ТСП(клиент<->сервер)
using System.Text;
using System.Net;

namespace Server
{

    class MyServer
    {
        TcpListener server;//                                                                   это "секретарь ждет звонка.держит трубку"=)

        public MyServer()
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 5555);//                      принимает подключение от клиента(ipadress принимает в себя стринг 127.0.0.1)
            server.Start();//                                                                    пришло подключение-сразу запускаем
            LoopClients();//                                                                     ловит клиентов(точнее входящий поток)

        }

        void LoopClients() //                                                                    0. каждый отдельный клиент - идет в отдельный поток через Threads
        {
            while (true)//
            {
                TcpClient client = server.AcceptTcpClient();//                                   1. принимает клиента и создает снизу поток и направляет его в поток
                Thread thread = new Thread(() => HandleClient(client));//                        2.  поток держит клиента(строка выше) через метод ниже HandleClient
                thread.Start();//                                                                3.  запустили поток.чтобы работал всегда и подхватывал клиентов.
            }
        }

        void HandleClient(TcpClient client)//                                                    удерживает client'a
        {
            //                                                                                   сервер один.клиентов много.для каждого клиента->свой отдельный поток
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.UTF8); //       хотиm получить сообщение от клиента(считать поток)
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.UTF8); //       хотиm ответить сообщением клиенту.
            while (true)
            {
                string massage = sReader.ReadLine();//                                            приняли строку (также как с консоли сообщение. но только через вход-ий поток)
                Console.WriteLine($" Входящее сообщение от клиента: {massage} ");

                Console.WriteLine(" Введите сообщение от Сервера-> Клиенту: ");
                string answer = Console.ReadLine();
                sWriter.WriteLine(answer);
                sWriter.Flush();
            }
        }
    }
}