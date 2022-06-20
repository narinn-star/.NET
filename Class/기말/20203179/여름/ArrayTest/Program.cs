using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ArrayTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList clients= new ArrayList(10);
		   
            client Aclient=new client();
            Aclient.Index=1; 
            Aclient.Socket=999; //Tcpclient instance
            Aclient.Nickname="Tom";
            clients.Add(Aclient);

            client Bclient=new client();
            Bclient.Index=2; 
            Bclient.Socket=777; //Tcpclient instance
            Bclient.Nickname="Jerry";
            clients.Add(Bclient);

            //foreach (client o in clients)
            //{
            //    Console.WriteLine(o.ToString());
            //}
            //Console.WriteLine("---------------");
            //clients.RemoveAt(0);
            //foreach (client o in clients)
            //{
            //    Console.WriteLine(o.ToString());
            //}


            foreach (client o in clients)
		    {
			    Console.WriteLine(o.ToString());
		    }
            
            Console.WriteLine("-----내용기반으로 항목을 찾아서 삭제--------");
            int DeleteIndex = clients.IndexOf(Aclient);
            clients.RemoveAt(DeleteIndex);
            foreach (client o in clients)
            {
                Console.WriteLine(o.ToString());
            }
        }
    }

    class client
    {
        public int Index { get; set; }
        public int Socket { get; set; }
        public string Nickname { get; set; }
        public string ToString()
        {
            return Index.ToString() + " " + Socket.ToString() + " " + Nickname;
        }
    }
}


