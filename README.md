# 4devAgendaParser
## Wstęp
Już w najbliższy poniedziałek (20 IV 2015) odbędzie się kolejna edycja konferencji 4developers (http://4developers.org.pl). Dostępnych będzie 15 ścieżek tematycznych w 10 blokach czasowych (dla niektórych ścieżkach niektóre bloki niewypełnione), co daje 135 różnych prelekcji z różnych obszarów, stąd wybór konkretnych jest bardzo trudny.

Na stronie konferencji prezentowany jest harmonogram konferencji 4developers, ale równocześnie wyświetlane są tylko trzy ścieżki (http://4developers.org.pl/pl/agenda/agenda/). Nie ma nawet wyboru wyświetlanych ścieżek, co utrudnia zaplanowanie planu konferencji.

Przydałby się widok pokazujący jednocześnie wszystkie ścieżki w danej godzinie, np. w postaci arkusza excela, albo tabeli html. Skoro organizator konferencji nie dostarczył takiej funkcjonalności, to dlaczego nie zrobić jej samemu?

## Dane wejściowe
Jak można się przekonać patrząc na źródło strony agendy, nie ma potrzeby osobnego pobierania danych dotyczących poszczególnych ścieżek, bo w kodzie strony są dane wszystkich ścieżek i żadne dane nie są pobierane z serwera przy przechodzeniu do kolejnych ścieżek.

## Pomysł
* pobrać kod strony za pomocą dotnetowego obiektu WebClient, 

* sparsować go przy użyciu wyrażeń regularnych, a następnie wygenerowanie xml-a ze wszystkim kolumnami, który być może będzie można łatwo zaimportować do Excela,

* alternatywnie wygenerować stronę html zawierającą tabelę zawierającą w kolumnach wszystkie ścieżki konferencji, a w wierszach wszystkie godziny.

## Wynik
* [Agenda 4developers 2015 w jednej tabeli](Agenda.html)