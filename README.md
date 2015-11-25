# SISE
Sztuczna Inteligencja i Systemy Ekspertowe

Zadanie 1 - przeszukiwanie przestrzeni stanów
Piętnastka

"Piętnastka", znana również pod angielską nazwą "Fifteen Puzzle", składa się z ramki, w której osadzone jest 15 klocków. Klocki można przesuwać, ponieważ w ramce pozostaje wolne miejsce o wielkości jednego klocka (zatem cała plansza ma wymiar 4x4). Gra polega na takim przesuwaniu klocków, aby z pewnego losowego układu początkowego (przykład poniżej)
	1 	2 	7
8 	9 	12 	10
13 	3 	6 	4
15 	14 	11 	5

uzyskać układ wzorcowy odpowiadający poniższemu:
1 	2 	3 	4
5 	6 	7 	8
9 	10 	11 	12
13 	14 	15 	
Cel zadania

Celem zadania jest napisanie programu, który będzie rozwiązywał powyższą łamigłówkę, czyli będzie wyznaczał taki ciąg ruchów, które przeprowadzą układankę z układu początkowego do układu wzorcowego. Należy przebadać, jak zachowują się w przypadku tego problemu różne metody przeszukiwania przestrzeni stanów:

    strategia "w głąb"
    strategia "wszerz"
    strategia "A*"

Dla ostatniej z wymienionych strategii należy zaproponować minimum 2 funkcje heurystyczne.
Wymagania funkcjonalne

Celem każdej grupy jest przygotowanie dwóch programów. Zadaniem pierwszego, weryfikowalnego na serwerze stud.ics.p.lodz.pl jest wczytanie ze standardowego wejścia układu początkowego łamigłówki, oraz wypisanie na standardowym wyjściu ciągu ruchów rozwiązującego tę łamigłówkę. Przyjmuje się że litera 'L' oznacza przesunięcie swobodnego klocka w lewo, P w prawo, G w górę, a D w dół . Wybór strategii odbywa się poprzez przekazanie odpowiednich parametrów w wierszu poleceń aplikacji.
Parametry wiersza poleceń:
  	

	-b/--bfs porządek 	Strategia przeszukiwania wszerz

	-d/--dfs porządek 	Strategia przeszukiwania w głąb

	-n/--nn id_heurystyki 	Strategia najpierw najlepszy

Gdzie porządek jest permutacją zbioru {'L','P','G','D'} określającą porządek przeszukiwania sąsiedztwa bieżącego stanu. Przykładowo napis DGLP oznacza porządek następujący porządek przeszukiwania: dół, góra, lewo, prawo. Jeżeli porządek zaczyna się od 'R' należy przyjąć kolejność losową (czytaj w każdym węźle grafu losujemy kolejność przeszukania).
Opis wejścia

W pierwszej linii wejścia znajdują się dwie liczby całkowite w k, odpowiednio pionowy (ilość wierszy) i poziomy (ilość kolumn) rozmiar ramki. Każdy z następnych w wierszy standardowego wyjścia zawiera k oddzielonych spacjami liczb całkowitych opisujących element układanki, przy czym wartość 0 oznacza pole puste.
Opis wyjścia

Na standardowym wyjściu powinny się pojawić dwie linie. Pierwsza powinna zawierać jedną liczbę całkowitą n określającą długość znalezionego rozwiązania (ilość kroków potrzebnych do rozwiązania łamigłówki). W drugiej zaś linijce powinien się znajdować napis długości n złożony z wielkich liter łacińskich ze zbioru {'L','P', 'G', 'D'} opisującego kolejne ruchy do wykonania.

Jeżeli dla danego układu początkowego nie istnieje rozwiązanie, w pierwszym i jedynym wierszy wyjścia powinna wię znajdywać liczba -1.
Aplikacja przeglądająca

Zadaniem drugiej aplikacji, jest wizualizowanie procesu rozwiązywania łamigłówki.

