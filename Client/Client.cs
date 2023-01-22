using System.Net.Sockets;//                                                            для библиотек для работы с ТСП(клиент<->сервер)
using System.Text;

// для справки TCP отвечает за передачу данных, а IP — за то, чтобы эти данные попали по нужному адресу;
namespace Client
{
    class MyClient
    {
        private TcpClient client;
        private StreamWriter sWriter;
        private StreamReader sReader;

        public MyClient() //                                                            метод создающий ip, port и поток(sreader записывает в себя данные)
        {
            client = new TcpClient("127.0.0.1", 5555); //                               (данные для клиента когда он появится.ip+port)
                                                       //                               прошло соединение и след ход это установка потока
            sWriter = new StreamWriter(client.GetStream(), Encoding.UTF8); //           импровезированный поток (этот поток для чтения)
            sReader = new StreamReader(client.GetStream(), Encoding.UTF8);//            импровезированный поток (этот поток для записи)
            HandleCommunication();//                                                    поддержка соединения(метод ниже)
        }
        void HandleCommunication()//                                                     держит мост потока неприрывным
        {

            while (true)
            {
                Console.WriteLine("> ");
                string message = Console.ReadLine();//                                   поключение к серверу который включен 
                sWriter.WriteLine(message);//                                            само сообщение от клиента серверу
                sWriter.Flush();//                                                       кнопка пуск-быстрая отправка.метод немедленной отправки сообщения
                string answerServer = sReader.ReadLine();
                Console.WriteLine($" Входящее сообщение от Сервера: {answerServer} ");
            }
        }
    }
}
