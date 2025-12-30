import { Component } from '@angular/core';
import { Game, GameService } from '../game.service';

@Component({
  selector: 'app-play',
  standalone: false,
  templateUrl: './play.component.html',
  styleUrl: './play.component.sass'
})
export class PlayComponent {

  lastGame: Game | null = null;
  loading = false;

  constructor(private gameService: GameService) {}


  play(choice: string) {
    this.loading = true;
    this.gameService.playGame(choice).subscribe({
      next: (game) => {
        this.lastGame = game;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error playing game:', err);
        this.loading = false;
      }
    });
  }


  getResultClass(): string {
    if (!this.lastGame) return '';
    
    switch(this.lastGame.result) {
      case 'Win': return 'text-success';
      case 'Loss': return 'text-danger';
      case 'Draw': return 'text-warning';
      default: return '';
    }
  }
}
