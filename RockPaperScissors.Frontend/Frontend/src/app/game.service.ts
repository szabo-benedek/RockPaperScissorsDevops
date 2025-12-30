import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from './environments/environment';

export interface Game {
  id: string;
  playerChoice: string;
  computerChoice: string;
  result: string;
  playedAt: Date;
}

export interface GameCreateDto {
  playerChoice: string;
}

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private apiUrl = environment.backendUrl + '/Game';
  private gamesSubject = new BehaviorSubject<Game[]>([]);
  games$ = this.gamesSubject.asObservable();

  constructor(private http: HttpClient) {
    console.log('GameService initialized');
    this.loadGames();
  }

  private loadGames() {
    console.log('Loading games...');
    this.http.get<Game[]>(this.apiUrl).subscribe({
      next: (games) => {
        console.log('Games loaded:', games);
        this.gamesSubject.next(games);
      },
      error: (err) => console.error('Error loading games:', err)
    });
  }

  playGame(choice: string): Observable<Game> {
    const dto: GameCreateDto = { playerChoice: choice };
    return this.http.post<Game>(this.apiUrl, dto).pipe(
      tap((game) => {
        console.log('Game played:', game);
        this.loadGames();
      })
    );
  }

  deleteAllGames(): Observable<any> {
    return this.http.delete(this.apiUrl).pipe(
      tap(() => {
        console.log('All games deleted');
        this.loadGames();
      })
    );
  }
}