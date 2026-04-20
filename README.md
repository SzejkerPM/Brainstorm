Brainstorm to prosta gra zręcznościowa stworzona w Godot 4.6 w celu zapoznania się z silnikiem oraz nauki C#.
Gracz ma za zadanie zbierać pozytywne, zielone myśli, a unikać przytłaczających, czerwonych. Gra szybko zwiększa swój poziom trudności, a sama rozgrywka skupia się bardziej na unikaniu, niż zbieraniu.

Główne funkcje gry:
- Płynne sterowanie z efektem squash and stretch dla soczystości rozgrywki
- Spawner, który zwiększa trudność rozgrywki i dba, aby spadające elementy nie nachodziły na siebie
- System zapisu gry wykorzystujący format binarny, którzy przechowuje rekord gracza
- Audio Manager z podziałem na buses
- Reaktywny UI/UX informujący o punktach, zdrowiu, a także proste menu startowe i końcowe

Technologie i wzorce:
- Signals and Events: komunikacja między skrpytami oparta na delegatach i sygnałach
- Static Services: wykorzystanie klas statycznych (SaveService) oraz wzorca Singleton (Autoload) dla menedżera dźwięku.
- Tweens: delikatne ożywienie sprite gracza
- Defensive programming: walidacja danych w setterach

Licencje i autorstwo:
- Silnik gry: Godot Engine - MIT
- Kod źródłowy: Udostępniony na licencji MIT
- Oprawa graficzna: Maciej Wilga
- Efekty dźwiękowe i muzyka: Pixabay, Royalty-free
