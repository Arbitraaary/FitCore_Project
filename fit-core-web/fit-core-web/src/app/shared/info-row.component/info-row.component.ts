import { Component, input } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-info-row',
  standalone: true,
  imports: [MatIconModule],
  templateUrl: './info-row.component.html',
  styleUrl: './info-row.component.scss',
})
export class InfoRowComponent {
  icon = input('info');
  label = input('');
  value = input('');
}
