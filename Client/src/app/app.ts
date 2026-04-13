import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { Navbar } from './core/navbar/navbar';
import { Loading } from "./core/loading/loading";

@Component({
  selector: 'app-root',
  imports: [FontAwesomeModule, Navbar, Loading, RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss',
  standalone: true,
})
export class App {
  protected readonly title = signal('Client');
}
