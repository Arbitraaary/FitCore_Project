import { Component, input } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';

@Component({
  selector: 'app-section-card',
  standalone: true,
  imports: [MatIconModule, MatDividerModule],
  templateUrl: './section-card.component.html',
  styleUrl: './section-card.component.scss',
})
export class SectionCardComponent {
  title = input('');
  icon = input('');
}
