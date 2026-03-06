import { Component, input, model, output, signal } from '@angular/core';
import { LucideAngularModule } from 'lucide-angular';

@Component({
  selector: 'app-button',
  imports: [LucideAngularModule],
  templateUrl: './button.html',
  styleUrl: './button.css',
})
export class Button {
  type = input<'button'|'submit'|'reset'>('button')
  onClick = output<MouseEvent>()
  fullWidth = input<boolean>(false)
  variant = input<'primary'|'secondary'|'primary-outline'|'secondary-outline'>('primary')
  isLoading = model<boolean>(false)
  disabled = input<boolean>(false)
  icon = input<string | null>(null)

  clickHandler(event: MouseEvent){
    this.onClick.emit(event)
  }
}
