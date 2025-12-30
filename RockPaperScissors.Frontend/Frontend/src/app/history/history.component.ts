import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Game, GameService } from '../game.service';

@Component({
  selector: 'app-history',
  standalone: false,
  templateUrl: './history.component.html',
  styleUrl: './history.component.sass'
})
export class HistoryComponent{
  games$: Observable<Game[]>;

  constructor(private gameService: GameService) {
    this.games$ = this.gameService.games$;
  }


  

  

  deleteAll() {
    if (confirm('Are you sure you want to delete all game history?')) {
      this.gameService.deleteAllGames().subscribe({
        next: () => {
          console.log('Games deleted successfully');
        },
        error: (err) => {
          console.error('Error deleting games:', err);
        }
      });
    }
  }

  getResultBadgeClass(result: string): string {
    switch(result) {
      case 'Win': return 'bg-success';
      case 'Loss': return 'bg-danger';
      case 'Draw': return 'bg-warning';
      default: return 'bg-secondary';
    }
  }
}
