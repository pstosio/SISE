using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SISE
{
    class Program
    {
        static void Main()
        {
            byte pozycja0, pozycjaN;
            byte kierunek0, kierunekN;
            byte[] tabStart = new byte[16] { 0, 2, 3, 4, 1, 6, 7, 8, 5, 10, 11, 12, 9, 13, 14, 15 }; //Tablica początkowa  
            byte[] tabN = new byte[16];
            byte[] tabRozw = new byte[16];

            // 1. Początkowy stan`
            wypisz(tabStart, "STAN POCZĄTKOWY\n\n ...nacisnij enter...");
            Console.ReadLine();

            // 2. Szukamy pozycji 0
            //pozycja0 = (byte)znajdzZero(tabStart);
            pozycja0 = (byte)znajdzZero(tabStart); 

            // 3. Możliwe kierunki ruchu na pozycji
            //dostepneRuchy(znajdzZero(tabStart));

            // 4. Znamy możliwe kierunki - losujemy kierunek ruchu
            kierunek0 = wybierzKierunek(dostepneRuchy(pozycja0));

            // 5. Wykonaj ruch we wskazanym kierunku
            
            tabN = rusz(tabStart, kierunek0, pozycja0);
            
            for (int i = 0; i < 1000000; i++)   //Ilośc pomieszań 
            {
                //Thread.Sleep(5);
                //wypisz(tabN);

                pozycjaN = (byte)znajdzZero(tabN);
                kierunekN = wybierzKierunek(dostepneRuchy(pozycjaN));
                tabStart = rusz(tabN, kierunekN, pozycjaN);

                //Thread.Sleep(5);
                if (i % 10000 == 0)
                    wypisz(tabStart, " !!! TRWA LOSOWE MIESZANIE TABLICY !!! ");

                pozycja0 = (byte)znajdzZero(tabStart);
                kierunek0 = wybierzKierunek(dostepneRuchy(pozycja0));
                tabN = rusz(tabStart, kierunek0, pozycja0);

            }
            
            wypisz(tabN, "TABLICA POMIESZANA \n\n ...nacisnij enter...");
            
            Console.ReadLine();

            //Sprobujemy przeszukac 'brutalnie'
            //wypisz(szukajBruteForce(tabN), "TRWA PRZESZUKIWANIE TABLICY");
            //wypisz(szukajBruteForce(tabStart), "TRWA PRZESZUKIWANIE TABLICY");

            //Spróbujemy przeszukać intelygentnie
            //wypisz(szukajAGwiazdka(tabN));
            //wypisz(szukajAGwiazdka(tabStart));

            // Algorytm 3 - Smart Brute
            wypisz(szukajSmartBrute(tabN));
            //wypisz(szukajSmartBrute(tabStart));

            Console.ReadLine();
        }
        #region Wypisz stan tablicy
        public static void wypisz(byte[] _tab, string _txt = "")
        {
            Console.Clear();
            Console.WriteLine("\n" + _txt +  "\n");

            for (byte i = 0; i < _tab.Length; i++ )
            {
                if (i % 4 == 0 && i != 0) Console.WriteLine();
                Console.Write(_tab[i] + " ");
            }
        }
        #endregion
        #region Pozycja zera
        /// <summary>
        /// Funkca zwraca pozycję na jakiej znajduje się 0
        /// </summary>
        /// <param name="_tab"></param>
        /// <returns></returns>
        public static byte znajdzZero(byte[] _tab)
        {
            for (byte i = 0; i < _tab.Length; i++)
            {
                if (_tab[i] == 0)
                    return (byte)(i + 1);
            }
            return 0;
        }
        /// <summary>
        /// Funkcja zwróci inta 1-9 w zależności od możliwego ruchu
        /// </summary>
        /// <param name="_poz"></param>
        #endregion
        #region Dostępny ruchy
        public static byte dostepneRuchy(byte _poz)
        {
            bool prawo = czyWPrawo(_poz);
            bool lewo = czyWLewo(_poz);
            bool dol = czyWDol(_poz);
            bool gora = czyWGore(_poz);

            if (prawo && lewo && dol && gora) return (byte)5;
            else if (prawo && lewo && dol) return (byte)2;
            else if (prawo && lewo && gora) return (byte)8;
            else if (gora && dol && prawo) return (byte)4;
            else if (gora && dol && lewo) return (byte)6;
            else if (dol && prawo) return (byte)1;
            else if (lewo && dol) return (byte)3;
            else if (gora && prawo) return (byte)7;
            else if (lewo && gora) return (byte)9;

            return 0;
        }
        #endregion
        #region Walidacja ruchu
        public static bool czyWPrawo(byte _poz)
        {
            byte[] tmp = new byte[4] { 4, 8, 12, 16 };
            for (byte i = 0; i < 4; i++)
                if (_poz == tmp[i]) return false;

            return true;
        }
        public static bool czyWLewo(byte _poz)
        {
            byte[] tmp = new byte[4] { 1, 5, 9, 13 };
            for (byte i = 0; i < 4; i++)
                if (_poz == tmp[i]) return false;

            return true;
        }
        public static bool czyWDol(byte _poz)
        {
            byte[] tmp = new byte[4] { 13, 14, 15, 16 };
            for (byte i = 0; i < 4; i++)
                if (_poz == tmp[i]) return false;
            
            return true;
        }
        public static bool czyWGore(byte _poz)
        {
            byte[] tmp = new byte[4] { 1, 2, 3, 4 };
            for (byte i = 0; i < 4; i++)
                if (_poz == tmp[i]) return false;

            return true;
        }
#endregion
        #region Losowanie Kierunku ruchu
        /// <summary>
        /// Funkcja wybiera losowo kierunek ruchu
        /// </summary>
        /// <param name="_tab"></param>
        /// <param name="_poz"></param>
        /// <returns> Zwracamy kierunek ruchu 1-4</returns>
        public static byte wybierzKierunek(byte _poz)
        {
            Random rand = new Random();
            byte tmp;
            switch (_poz)
            {
                case 1:
                    tmp = (byte)rand.Next(0, 2);
                    if (tmp == 0)
                        return(byte) 2;
                    if (tmp == 1)
                        return (byte)3;
                    break;

                case 2:
                     tmp = (byte)rand.Next(0, 3);
                    if (tmp == 0)
                        return(byte) 2;
                    if (tmp == 1)
                        return (byte)3;
                    if (tmp == 2)
                        return (byte)4;
                    break;

                case 3:
                    tmp = (byte)rand.Next(0, 2);
                    if (tmp == 0)
                        return(byte) 3;
                    if (tmp == 1)
                        return (byte)4;
                    break;

                case 4:
                    tmp = (byte)rand.Next(0, 3);
                    if (tmp == 0)
                        return(byte) 1;
                    if (tmp == 1)
                        return (byte)2;
                    if (tmp == 2)
                        return (byte)3;
                    break;

                case 5:
                    tmp = (byte)rand.Next(0, 4);
                    if (tmp == 0)
                        return(byte) 1;
                    if (tmp == 1)
                        return (byte)2;
                    if (tmp == 2)
                        return (byte)3;
                    if (tmp == 3)
                        return (byte)4;
                    break;

                case 6:
                    tmp = (byte)rand.Next(0, 3);
                    if (tmp == 0)
                        return(byte) 1;
                    if (tmp == 1)
                        return (byte)3;
                    if (tmp == 2)
                        return (byte)4;
                    break;

                case 7:
                    tmp = (byte)rand.Next(0, 2);
                    if (tmp == 0)
                        return(byte) 1;
                    if (tmp == 1)
                        return (byte)2;
                    break;

                case 8:
                    tmp = (byte)rand.Next(0, 3);
                    if (tmp == 0)
                        return(byte) 4;
                    if (tmp == 1)
                        return (byte)1;
                    if (tmp == 2)
                        return (byte)2;
                    break;

                case 9:
                    return rand.Next(0, 2) == 0 ? (byte)1 : (byte)4;
            }
            return 0;
        }
        #endregion
        #region Wykonanie ruchu i zwrócenie stanu
        public static byte[] rusz(byte[] _tab, byte _kierunek, byte _poz0)
        {
            switch (_kierunek)
            {
                case 1:
                    return ruchGora(_tab, _poz0);
                case 2:
                    return ruchPrawo(_tab, _poz0);
                case 3:
                    return ruchDol(_tab, _poz0);
                case 4:
                    return ruchLewo(_tab, _poz0);
            }
            return new byte[0];
        }
        public static byte[] ruchGora(byte[] _tab, byte _poz0)
        {
            byte[] tmp = new byte[16];

            for (int i = 0; i < _tab.Length; i++)
            {
                tmp[i] = _tab[i];
            }

            tmp[_poz0 - 1] = tmp[_poz0 - 4 - 1];
            tmp[_poz0 - 4 - 1] = 0;            //Nowa pozycja 0
            
            return tmp;
        }
        public static byte[] ruchDol(byte[] _tab, byte _poz0)
        {
            byte[] tmp = new byte[16];

            for (int i = 0; i < _tab.Length; i++)
            {
                tmp[i] = _tab[i];
            }

            tmp[_poz0 - 1] = tmp[_poz0 + 4 - 1];
            tmp[_poz0 + 4 - 1] = 0;            //Nowa pozycja 0

            return tmp;
        }
        public static byte[] ruchLewo(byte[] _tab, byte _poz0)
        {
            byte[] tmp = new byte[16];

            for (int i = 0; i < _tab.Length; i++ )
            {
                tmp[i] = _tab[i];
            }

            tmp[_poz0 - 1] = tmp[_poz0 - 1 - 1];
            tmp[_poz0 - 1 - 1] = 0;            //Nowa pozycja 0

            return tmp;
        }
        public static byte[] ruchPrawo(byte[] _tab, byte _poz0)
        {
            byte[] tmp = new byte[16];

            for (int i = 0; i < _tab.Length; i++)
            {
                tmp[i] = _tab[i];
            }

            tmp[_poz0 - 1] = tmp[_poz0];
            tmp[_poz0] = 0;            //Nowa pozycja 0

            return tmp;
        }
        #endregion
        #region Sprawdzenie czy znaleziono rozwiazanie
        public static bool sprawdz(byte[] _tab)
        {
            byte[] rozwiazanie = new byte[16] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0 };

            if (_tab.SequenceEqual<byte>(rozwiazanie))
                return true;

            return false;
        }
        #endregion
        #region F-cja zwroci mozliwe kierunki ruchu
        public static byte[] zwrocRuchy(byte _poz0)
        {
            byte[] tmp = null; // Bo maksymalnie 4 mozliwe ruchy

            switch (_poz0)
            {
                case (1):
                    tmp = new byte[2] { 2, 3 }; // OK !
                    break;

                case (2):
                    tmp = new byte[3] { 2, 3, 4 }; // OK !
                    break;

                case (3):
                    tmp = new byte[2] { 3, 4 }; // OK!
                    break;

                case (4):
                    tmp = new byte[3] { 1, 2, 3 }; // OK!
                    break;

                case (5):
                    tmp = new byte[4] { 1, 2, 3, 4 }; // OK!
                    break;

                case (6):
                    tmp = new byte[3] { 1, 3, 4 }; // OK!
                    break;

                case (7):
                    tmp = new byte[2] { 1, 2 }; // OK!
                    break;

                case (8):
                    tmp = new byte[3] { 1, 2, 4 }; // OK!
                    break;

                case (9):
                    tmp = new byte[2] { 1, 4 }; // OK!
                    break;
            }

            return tmp;
        }

        #endregion
        #region F-cja zwroci koszt ruchu węzła - metoda liczy brakujące elementy układanki - METODA "NA ILOŚC POPRAWNYCH POZYCJI"
        public static byte zwrocKoszt(byte[] _tmp)
        {
            byte heristicCost = 0;

            for (int i = 0; i < _tmp.Length; i++)
            {
                int value = _tmp[i] - 1;

                if (value != 0 && value != i)
                {
                    heristicCost++;
                }

            }

            return heristicCost;
        }
        #endregion
        #region  F-cja zwroci koszt ruchu węzła - METODA MANHATTAN DISTANCE
        public static byte zwrocKoszt_Mnht(byte[] _tmp)
        {
            byte heuristicCost = 0;
            int gridX = (int)Math.Sqrt(_tmp.Length); // W zasadzie moze byc 16 "na szwtyno"
            int idealX;
            int idealY;
            int currentX;
            int currentY;
            int value;

            for (int i = 0; i < _tmp.Length; i ++ )
            {
                value = _tmp[i] - 1;

                if (value == -1)
                {
                    value = _tmp.Length - 1;
                    //////////
                }

                if (value != i)
                {
                    idealX = value % gridX;
                    idealY = value / gridX;

                    currentX = i % gridX;
                    currentY = i / gridX;

                    heuristicCost += (byte)(Math.Abs(idealY - currentY) + Math.Abs(idealX - currentX));
                }
            }

                return heuristicCost;
        }
        #endregion
        #region F-cja zwraca wartość logiczną w zależności czy istnieje węzeł
        public static bool czyIstnieje(List<byte[]> _list, byte[] _node)
        {
            int licznik = 0;
            bool exists = false;
            byte[] tab = null;

            _list.Reverse(0, _list.Count / 4 );

            for (int i = 0; i < _list.Count && i < 10000; i++ ) // Maks 10 tys ruchow wstecz - maleje wydajnosc z czasem dzialania programu  
            {
                tab = _list[i];

                for (int j = 0; j < 16; j++)
                {
                    if (tab[j] == _node[j])
                        licznik++;

                    if (licznik == 15)
                    {
                        Console.Write("!");
                        return true;
                    }
                }

                licznik = 0;
            }

            return false;
        }
        #endregion
        #region F-cja zwraca wartość logcziną w zależności czy istnieje węzeł - Lepsza wydajność !!!
        public static bool czyIstniejeBinary(List<byte[]> _list, byte[] _node)
        {
            if (_list.Count > 1)
            {
                int i = _list.BinarySearch(_node);
            }

            return false;
        }
        #endregion
        #region F-cja SmartBrute - zwroci indeks z iloscia posortowanych el - 1, 2, 3, 4, 5, 9, 13
        public static byte zwrocIndeksUlozonych(byte[] _node)
        {
            byte indeks = 0;

            if (_node[0] == 1)
                indeks++;
            else
                return indeks;

            if (_node[0] == 1 && _node[1] == 2)
                indeks++;
            else
                return indeks;

            if (_node[0] == 1 && _node[1] == 2 && _node[2] == 3)
                indeks++;
            else
                return indeks;

            if (_node[0] == 1 && _node[1] == 2 && _node[2] == 3 && _node[3] == 4)
                indeks++;
            else
                return indeks;

            if (_node[0] == 1 && _node[1] == 2 && _node[2] == 3 && _node[3] == 4 && _node[4] == 5)
                indeks++;
            else
                return indeks;

            if (_node[0] == 1 && _node[1] == 2 && _node[2] == 3 && _node[3] == 4 && _node[4] == 5 && _node[8] == 9)
                indeks++;
            else
                return indeks;

            if (_node[0] == 1 && _node[1] == 2 && _node[2] == 3 && _node[3] == 4 && _node[4] == 5 && _node[8] == 9 && _node[12] == 13)
                indeks++;

            return indeks;
        }
        #endregion
        #region F-cja SmartBrute - dostanie indeks rodzica i zwroci czy mozna wykonac wezel
        public static bool isNodeExecutable(byte _index, byte[] _node, bool _isValid = false)
        {
            bool ret = true;

            if (_isValid)
            {
                if (_index == 3 && _node[3] != 4)
                    return ret = true;

                if (_index == 6 && _node[12] != 13)
                    return ret = true;
            }
            else
            {
                switch (_index)
                {
                    case (1):
                        if (_node[0] == 0) ret = false;
                        break;

                    case (2):
                        if (_node[0] == 0 || _node[1] == 0) ret = false;
                        break;

                    case (3):
                        if (_node[0] == 0 || _node[1] == 0 || _node[2] == 0) ret = false;
                        break;

                    case (4):
                        if (_node[0] == 0 || _node[1] == 0 || _node[2] == 0 || _node[3] == 0) ret = false;
                        break;

                    case (5):
                        if (_node[0] == 0 || _node[1] == 0 || _node[2] == 0 || _node[3] == 0 || _node[4] == 0) ret = false;
                        break;

                    case (6):
                        if (_node[0] == 0 || _node[1] == 0 || _node[2] == 0 || _node[3] == 0 || _node[4] == 0 || _node[8] == 0) ret = false;
                        break;

                    case (7):
                        if (_node[0] == 0 || _node[1] == 0 || _node[2] == 0 || _node[3] == 0 || _node[4] == 0 || _node[8] == 0 || _node[12] == 0) ret = false;
                        break;
                }
            }

            return ret;
        }
        #endregion
        #region F-cja SmartBrute - dostanie node'a i sprawdzi czy nie ustawi 3 bez strategii
        public static bool isNodeExecutableWithoutThree(byte[] _node)
        {
            bool ret = true;

            if (_node[0] == 0 || _node[1] == 0 || _node[2] == 3 ) ret = false;

            return ret;
        }
        #endregion
        #region F-cja SmartBrute - dostanie node'a i sprawdzi czy nie ustawi 9 bez strategii
        public static bool isNodeExecutableWithoutNine(byte[] _node)
        {
            bool ret = true;

            if (_node[0] == 0 || _node[1] == 0 || _node[2] == 0 || _node[3] == 0 || _node[4] == 0 || _node[8] == 9) ret = false;

            return ret;
        }
        #endregion
        #region F-cja SmartBrute - dostanie node'a i sprawdzi czy wykonac ruch aby ulozyc 3 i 4
        public static bool isExecThreeAndFour(byte[] _node)
        {
            bool ret = false;

            if (_node[2] == 4 && _node[3] == 0 && _node[6] == 3)
                ret = true;

            return ret;
        }
        #endregion
        #region F-cja SmartBrute - dostanie node'a i sprawdzi czy wykonac ruch aby ulozyc 9 i 13
        public static bool isExecNineAndThirteen(byte[] _node)
        {
            bool ret = false;

            if (_node[8] == 0 && _node[12] == 9 && _node[13] == 13)
                ret = true;

            return ret;
        }
        #endregion
        #region Algorytm 1 - Losowy Brute Force - przeszuka 8-mke, na 15 za słaby, zajęłoby mu to średnio jakieś 7 lat :)
        public static byte[] szukajBruteForce(byte[] _tab)
        {
            byte[] tmpTab = new byte[16];
            byte[] tmpTab2 = new byte[16];
            int licznik = 0 ;
            byte tmp0 = (byte)znajdzZero(_tab);
            byte tmp02;
            byte tmpKier = (byte)wybierzKierunek(dostepneRuchy(tmp0));
            byte tmpKier2;

            tmpTab = _tab;

            while (sprawdz(tmpTab) != true)
            {
                licznik++;
                tmp02 = (byte)znajdzZero(tmpTab);
                tmpKier2 = (byte)wybierzKierunek(dostepneRuchy(tmp02));

                tmpTab2 = rusz(tmpTab, tmpKier2, tmp02);   //Losowy ruch
                tmpTab = tmpTab2;

                if (licznik % 10000000 == 0) //Pokaze co milionowe ustawienie :)
                    wypisz(tmpTab2, "TRWA WYSZUKIWANIE METODĄ: BRUTE FORCE");
            }
            if (sprawdz(tmpTab) == true)
            {
                wypisz(tmpTab, " HURRA !!!! ZNALEZIONO ROZWIĄZANIE !!!!");
                Console.ReadKey();
                Environment.Exit(1);
            }

            return tmpTab;
        }

        #endregion
        #region Algorytm 2 - Przeszukiwanie drzewa możliwości - niedokonczony
        public static byte[] szukajAGwiazdka(byte[] _tab)
        {
            byte[] tmpKierunkiTab = null;

            byte[] tmpTab = new byte[16];
            byte[] tmpTab2 = new byte[16];
            
            int licznik = 0;
 
            byte pozZero = (byte)znajdzZero(_tab);
            byte pozZero2;

            byte tmpKierunki;
            byte tmpKierunki2;

            List<byte[]> listaClosedStany = new List<byte[]>();
            List<byte[]> listaOpenStany = new List<byte[]>();

            //byte[] kosztyWezlow = new byte[4];
            List<byte> kosztyWezlow = new List<byte>();
            byte indexKosztow = 0;
            byte licznikKosztow = 0;

            tmpTab = _tab; // Podstawienie parametru

            while (sprawdz(tmpTab) != true) // Sprawdzam czy rozwiazane
            {

                /*
                 * 1 Znaleźć możliwe ruchy 
                 * 2 Sprawdzic czy ruch prowadzi do stanu ktory juz jest na liscie
                 * 3 Jezeli jest to odrzucic
                 * 4 Policzyc wagi dla pozostalych ruchow - HEURESTYKA !!!
                 * 5 Wybrać optymalny ruch
                 * 6 Wykonać ten ruch
                 */

                licznik++; // licznik pętli

                pozZero2 = (byte)znajdzZero(tmpTab);    // Pozycja zera
                tmpKierunki2 = dostepneRuchy(pozZero2); // Dostaję liczbe 1 - 9
                
                // Funkcja dostanie liczbe i zwroci mozliwe kierunki na tabliczce o rozmiarze 1 - 4
                tmpKierunkiTab = zwrocRuchy(tmpKierunki2);

                // Pętla po dostępnych kierunkach - uzyskujemy węzły
                for (int i = 0; i < tmpKierunkiTab.Length; i++ )
                {
                    // Sprawdzam czy stanu nie ma na liście stanow zamknietych
                    if (czyIstnieje(listaClosedStany, rusz(tmpTab, tmpKierunkiTab[i], pozZero2)) == false) 
                    {
                        // Wykonujemy ruch i zapisujemy stan na liście stanów otwartych
                        listaOpenStany.Add(rusz(tmpTab, tmpKierunkiTab[i], pozZero2));
                    }
                }

                // Przeszukujemy listę otwartych stanow, liczymy dla kazdego koszt
                for (int i = 0; i < listaOpenStany.Count; i++ )
                {
                    // Wrzucamy node'a do f-cji ktora zwroci nam koszt ruchu
                    kosztyWezlow.Add(zwrocKoszt_Mnht(listaOpenStany[i]));
                }

                // Jeśli lista open stanów jest pusta, bo przeszukaliśmy węzły - wrzucamy losowy ruch
                if (listaOpenStany.Count == 0)
                {
                    Random rand = new Random();                   // Punkt newralgiczny - zwiększa losowość
                    int i = rand.Next(0, tmpKierunkiTab.Length);
                   
                    listaOpenStany.Add(rusz(tmpTab, tmpKierunkiTab[i], pozZero2));

                    // Licze koszt dla tego jednego stanu
                    kosztyWezlow.Add(zwrocKoszt_Mnht(listaOpenStany[0]));
                }


                // Wybierz węzeł (index) o najmniejszym koszcie 
                indexKosztow = (byte)kosztyWezlow.IndexOf(kosztyWezlow.Min());
                
                // Wrzucam wybrany ruch na liste ranow odwiedzonych
                try
                {
                    listaClosedStany.Add(listaOpenStany[indexKosztow]);
                }
                catch (System.ArgumentOutOfRangeException ex)
                {
                   
                }
                finally
                {
                    Random random = new Random();
                    listaOpenStany.Add(rusz(tmpTab, tmpKierunkiTab[random.Next(0,2)], pozZero2));
                }
                try
                {
                    tmpTab = listaOpenStany[indexKosztow];
                }
                catch (System.ArgumentOutOfRangeException ex)
                {

                }
                finally
                {
                    
                }

                //Czyszczenie zmiennych tymczasowych
                listaOpenStany.Clear();
                kosztyWezlow.Clear();
                indexKosztow = 0;
                licznikKosztow = 0;
                
                Thread.Sleep(800);
                if (licznik % 1 == 0) //Pokaze co ktores ustawienie
                {
                    wypisz(tmpTab, "TRWA WYSZUKIWANIE METODĄ: A GWIAZDKA");
                    Console.WriteLine();
                    
                    Console.WriteLine("\n\nDlugosc tablicy stanow przeszukanych: {0}", listaClosedStany.Count);
                }
            }
            if (sprawdz(tmpTab) == true)
            {
                wypisz(tmpTab, " HURRA !!!! ZNALEZIONO ROZWIĄZANIE !!!!");
                Console.WriteLine("\n\nDlugosc tablicy stanow przeszukanych: {0}", listaClosedStany.Count);
                Console.ReadKey();
                Environment.Exit(1);
            }

            return tmpTab;
        }
        #endregion

        #region Algorytm 3 - Smart Brute
        public static byte[] szukajSmartBrute(byte[] _tab)
        {
            byte[] tmpKierunkiTab = null;

            byte[] tmpTab = new byte[16];
            byte[] tmpTab2 = new byte[16];

            int licznik = 0;

            byte pozZero = (byte)znajdzZero(_tab);
            byte pozZero2;

            byte tmpKierunki;
            byte tmpKierunki2;

            List<byte[]> listaClosedStany = new List<byte[]>();
            List<byte[]> listaOpenStany = new List<byte[]>();

            //byte[] kosztyWezlow = new byte[4];
            List<byte> kosztyWezlow = new List<byte>();
            byte indexKosztow = 0;
            byte licznikKosztow = 0;

            byte indeksUlozonych = 0;
            Random rand = new Random();
            int randValue = 0;
            int tabCount = 0;
            bool strategy1 = false;
            bool strategy2 = false;

            tmpTab = _tab; // Podstawienie parametru

            while (sprawdz(tmpTab) != true) // Sprawdzam czy rozwiazane
            {

                licznik++; // licznik pętli

                pozZero2 = (byte)znajdzZero(tmpTab);    // Pozycja zera
                tmpKierunki2 = dostepneRuchy(pozZero2); // Dostaję liczbe 1 - 9

                // Funkcja dostanie liczbe i zwroci mozliwe kierunki na tabliczce o rozmiarze 1 - 4
                tmpKierunkiTab = zwrocRuchy(tmpKierunki2);

                // Pętla po dostępnych kierunkach - uzyskujemy węzły
                for (int i = 0; i < tmpKierunkiTab.Length; i++)
                {
                        // Wykonujemy ruch i zapisujemy stan na liście stanów otwartych
                        listaOpenStany.Add(rusz(tmpTab, tmpKierunkiTab[i], pozZero2));      // OK - mam węzły
                }

                // Dla rodzica musze sprawdzic (dostac indeks) ustawienie pol 1, 2, 3, 4, 5, 9, 13
                indeksUlozonych = zwrocIndeksUlozonych(tmpTab);

                if (indeksUlozonych == 2) // Strategia gdy mamy ulozone 1 i 2
                {
                    tabCount = listaOpenStany.Count;
                    for (int i = 0; i < tabCount; i++)
                    {
                        if (isExecThreeAndFour(listaOpenStany[i]))
                        {
                            listaClosedStany.Add(listaOpenStany[i]);
                            tmpTab = listaOpenStany[i];
                            strategy1 = true;

                            // Trzeba pokierowac na sztywno kolejnym ruchem
                            // 1. W lewo (4), pozycja 0- wiadomo, ze [3] - 
                            // 2. W dół

                            wypisz(tmpTab, "TRWA WYSZUKIWANIE METODĄ: SMART BRUTE");
                            
                            listaOpenStany.Clear();
                            listaOpenStany.Add(rusz(tmpTab, 4, 4));      // OK - poszedlem w lewo
                            listaClosedStany.Add(listaOpenStany[0]); // !!
                            tmpTab = listaOpenStany[0];

                            wypisz(tmpTab, "TRWA WYSZUKIWANIE METODĄ: SMART BRUTE");
                            
                            listaOpenStany.Clear();
                            listaOpenStany.Add(rusz(tmpTab, 3, 3));      // OK - teraz w dol
                            listaClosedStany.Add(listaOpenStany[0]); // !!
                            tmpTab = listaOpenStany[0];

                            wypisz(tmpTab, "TRWA WYSZUKIWANIE METODĄ: SMART BRUTE");
                            
                            break;
                        }   
                    }

                    if (!strategy1)
                    {
                        for (int i = 0; i < tabCount; i++)
                        {
                            if (!isNodeExecutableWithoutThree(listaOpenStany[i]))
                            {
                                listaOpenStany.Remove(listaOpenStany[i]);
                                tabCount--;
                            }
                        }
                        // Teraz musze wziac losowy stan i wykonac ruch
                        randValue = rand.Next(0, listaOpenStany.Count);

                        listaClosedStany.Add(listaOpenStany[randValue]);
                        tmpTab = listaOpenStany[randValue];
                    }
                    
                }
                else if (indeksUlozonych == 3) // + Walidacja
                {
                    // W zaleznosci od indeksu musze sprawdzac czy moge wykonac wezel
                    // Petla po wezlach , sprawdzam czy moge wykonac, jak moge to robie i wychodze z f-cji
                    tabCount = listaOpenStany.Count;
                    for (int i = 0; i < tabCount; i++)
                    {
                        if (!isNodeExecutable(indeksUlozonych, listaOpenStany[i], true))
                        {
                            listaOpenStany.Remove(listaOpenStany[i]);
                            tabCount--;
                        }
                    }


                    // Teraz musze wziac losowy stan i wykonac ruch
                    randValue = rand.Next(0, listaOpenStany.Count);

                    listaClosedStany.Add(listaOpenStany[randValue]);
                    tmpTab = listaOpenStany[randValue];
                }
                else if (indeksUlozonych == 5) // Strategia gdy mamy ulozone 1 2 3 4 5
                {
                    tabCount = listaOpenStany.Count;
                    for (int i = 0; i < tabCount; i++)
                    {
                        if (isExecNineAndThirteen(listaOpenStany[i]))
                        {
                            listaClosedStany.Add(listaOpenStany[i]);
                            tmpTab = listaOpenStany[i];
                            strategy2 = true;

                            // Mamy stan, wystarczy go dalej na sztywno pokierować
                            wypisz(tmpTab, "TRWA WYSZUKIWANIE METODĄ: SMART BRUTE");
                            listaOpenStany.Clear();
                            
                            listaOpenStany.Add(rusz(tmpTab, 3, 9));      // OK - poszedlem w dol
                            listaClosedStany.Add(listaOpenStany[0]);
                            tmpTab = listaOpenStany[0];

                            wypisz(tmpTab, "TRWA WYSZUKIWANIE METODĄ: SMART BRUTE");
                            listaOpenStany.Clear();

                            listaOpenStany.Add(rusz(tmpTab, 2, 13));      // OK - teraz w prawo
                            listaClosedStany.Add(listaOpenStany[0]);
                            tmpTab = listaOpenStany[0];

                            wypisz(tmpTab, "TRWA WYSZUKIWANIE METODĄ: SMART BRUTE");
                            listaOpenStany.Clear();

                            break;
                        }
                    }

                    if (!strategy2)
                    {
                        for (int i = 0; i < tabCount; i++)
                        {
                            if (!isNodeExecutableWithoutNine(listaOpenStany[i]))
                            {
                                listaOpenStany.Remove(listaOpenStany[i]);
                                tabCount--;
                            }
                        }
                        // Teraz musze wziac losowy stan i wykonac ruch
                        randValue = rand.Next(0, listaOpenStany.Count);

                        listaClosedStany.Add(listaOpenStany[randValue]);
                        tmpTab = listaOpenStany[randValue];
                    }
                }
                else if (indeksUlozonych == 6) // + Walidacja
                {
                    // W zaleznosci od indeksu musze sprawdzac czy moge wykonac wezel
                    // Petla po wezlach , sprawdzam czy moge wykonac, jak moge to robie i wychodze z f-cji
                    tabCount = listaOpenStany.Count;
                    for (int i = 0; i < tabCount; i++)
                    {
                        if (!isNodeExecutable(indeksUlozonych, listaOpenStany[i], true))
                        {
                            listaOpenStany.Remove(listaOpenStany[i]);
                            tabCount--;
                        }
                    }


                    // Teraz musze wziac losowy stan i wykonac ruch
                    randValue = rand.Next(0, listaOpenStany.Count);

                    listaClosedStany.Add(listaOpenStany[randValue]);
                    tmpTab = listaOpenStany[randValue];
                }
                else // Dla każdego innego indeksu ułożenia
                {
                    // W zaleznosci od indeksu musze sprawdzac czy moge wykonac wezel
                    // Petla po wezlach , sprawdzam czy moge wykonac, jak moge to robie i wychodze z f-cji
                    tabCount = listaOpenStany.Count;
                    for (int i = 0; i < tabCount; i++)
                    {
                        if (!isNodeExecutable(indeksUlozonych, listaOpenStany[i]))
                        {
                            listaOpenStany.Remove(listaOpenStany[i]);
                            tabCount--;
                        }
                    }


                    // Teraz musze wziac losowy stan i wykonac ruch
                    randValue = rand.Next(0, listaOpenStany.Count);

                    listaClosedStany.Add(listaOpenStany[randValue]);
                    tmpTab = listaOpenStany[randValue];
                }

                /*

                    // Przeszukujemy listę otwartych stanow, liczymy dla kazdego koszt
                    for (int i = 0; i < listaOpenStany.Count; i++)
                    {
                        // Wrzucamy node'a do f-cji ktora zwroci nam koszt ruchu
                        kosztyWezlow.Add(zwrocKoszt_Mnht(listaOpenStany[i]));
                    }

                // Jeśli lista open stanów jest pusta, bo przeszukaliśmy węzły - wrzucamy losowy ruch
                if (listaOpenStany.Count == 0)
                {
                    Random rand = new Random();                   // Punkt newralgiczny - zwiększa losowość
                    int i = rand.Next(0, tmpKierunkiTab.Length);

                    listaOpenStany.Add(rusz(tmpTab, tmpKierunkiTab[i], pozZero2));

                    // Licze koszt dla tego jednego stanu
                    kosztyWezlow.Add(zwrocKoszt_Mnht(listaOpenStany[0]));
                }


                // Wybierz węzeł (index) o najmniejszym koszcie 
                indexKosztow = (byte)kosztyWezlow.IndexOf(kosztyWezlow.Min());

                // Wrzucam wybrany ruch na liste ranow odwiedzonych
                try
                {
                    listaClosedStany.Add(listaOpenStany[indexKosztow]);
                }
                catch (System.ArgumentOutOfRangeException ex)
                {

                }
                finally
                {
                    Random random = new Random();
                    listaOpenStany.Add(rusz(tmpTab, tmpKierunkiTab[random.Next(0, 2)], pozZero2));
                }
                try
                {
                    tmpTab = listaOpenStany[indexKosztow];
                }
                catch (System.ArgumentOutOfRangeException ex)
                {

                }
                finally
                {

                }
                */
                //Czyszczenie zmiennych tymczasowych
                listaOpenStany.Clear();
                kosztyWezlow.Clear();
                indexKosztow = 0;
                licznikKosztow = 0;
                strategy1 = false;
                strategy2 = false;

                //Thread.Sleep(50);
                if (licznik % 1000 == 0) //Pokaze co ktores ustawienie
                {
                    //Thread.Sleep(50);
                    wypisz(tmpTab, "TRWA WYSZUKIWANIE METODĄ: SMART BRUTE");
                    Console.WriteLine();

                    Console.WriteLine("\n\nDlugosc tablicy stanow przeszukanych: {0}", listaClosedStany.Count);
                }
            }
            if (sprawdz(tmpTab) == true)
            {
                wypisz(tmpTab, " HURRA !!!! ZNALEZIONO ROZWIĄZANIE !!!!");

                Console.WriteLine("\n\nDlugosc tablicy stanow przeszukanych: {0}", listaClosedStany.Count);
                Console.ReadKey();
                Environment.Exit(1);
            }

            return tmpTab;
        }
        #endregion
    }
}
