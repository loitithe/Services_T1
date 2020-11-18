using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
/**
 * Ejercicio 4
    Realiza en consola el juego de carreras de caballos con al menos 5 caballos (haz un array de hilos)
    pero teniendo en cuenta que ahora cada caballo es un thread. El usuario hace su apuesta y luego
    empieza la carrera de caballos de forma que cada uno se mueve una distancia aleatoria y “duerme”
    un tiempo aleatorio.
    Nota: De cara a realizar pruebas de este juego, se recomienda quitar la aleatoriedad temporalmente para forzar a que
    varios caballos lleguen a un tiempo y ver que solo uno es el que “cruza” la meta.
    (Opcional) Añade aparición aleatoria de “sobre sorpresa” que aceleren o retarden el caballo que lo
    coge o que pause temporalmente a otros caballos.
    (Opcional) Hazlo en Java multihilo.
 */
//static bool run;
//static int cont = 0;

namespace HorseRace
{

    class Program
    {
        static int RandMove()
        {
            Random rm = new Random();
            int speed = rm.Next(0, 3);
            int s = 0;
            switch (speed)
            {
                case int n when speed == 1:
                    s = 1;
                    break;
                case int n when speed == 2:
                    s = 2;
                    break;
                case int n when speed == 3:
                    s = 3;
                    break;
                case int n when speed == 0:
                    s = 0;
                    break;
            }
            return s;
        }

        static int RandSleep()
        {
            Random rm = new Random();
            int varSleep = rm.Next(1, 14);
            int s = 0;
            switch (varSleep)
            {
                case int n when varSleep >= 1 || varSleep < 4:
                    s = 30;
                    break;
                case int n when varSleep >= 4 || varSleep < 7:
                    s = 27;
                    break;
                case int n when varSleep >= 7 || varSleep < 11:
                    s = 32;
                    break;
                case int n when varSleep >= 11 || varSleep < 14:
                    s = 35;
                    break;

            }
            return s;
        }
        static void Move(int numTrack)
        {
            int position = 3;
            int move = 0;
            race = true;
            string ast = "*";
            string space = " ";
            int end = Console.WindowWidth - 10;
            lock (l)
            {
                Console.SetCursorPosition(end, numTrack);
                Console.Write("||");
            }
            int aux = 3;
            while (race)
            {
                lock (l)
                {
                    if (race)
                    {
                        if (aux == 3)
                        {
                            Console.SetCursorPosition(aux, numTrack);
                            Console.Write("{0,1}", ast);
                            Console.SetCursorPosition(aux - 1, numTrack);
                            Console.Write("{0,1}", space);
                        }
                        else if (aux != 3)
                        {
                            move = RandMove();
                            position += move;
                            Console.SetCursorPosition(position, numTrack);
                            Console.Write("{0,1}", ast);
                            Console.SetCursorPosition(position - move, numTrack);
                            Console.Write("{0,1}", space);
                        }

                        if (aux == end || position >= end)
                        {
                            winner = Console.CursorTop - 2;
                            Console.SetCursorPosition(25, 10);
                            Console.Write("Race End!! \tThe horse {0} wins", winner);
                            race = false;

                        }

                        aux++;
                    }
                }
                Thread.Sleep(RandSleep());
            }

            Console.SetCursorPosition(25, 12);
            if (bet == winner)
            {
                Console.WriteLine("Your horse win!! ");
            }
            else
            {
                Console.WriteLine("Try again! ");
            }


            Console.ReadKey();
        }
        static int winner;
        static int bet;
        static bool race;
        static readonly private object l = new object();
        static void Main(string[] args)
        {
            


            Console.WriteLine("Choose a horse");
            Thread[] arrThreads = new Thread[5];
            for (int i = 0; i < arrThreads.Length; i++)
            {
                Console.WriteLine("Horse {0}", i + 1);
            }
            try
            {
                bet = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Choose a available horse");
            }
            Console.Clear();

            for (int i = 0; i < arrThreads.Length; i++)
            {
                int numTrack = i + 3;
                arrThreads[i] = new Thread(() => Move(numTrack));
                arrThreads[i].Priority = ThreadPriority.Normal;
            }

            for (int i = 0; i < arrThreads.Length; i++)
            {
                arrThreads[i].Start();

            }
        }
    }
}
