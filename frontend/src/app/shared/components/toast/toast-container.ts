import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { LucideAngularModule } from 'lucide-angular';
import { Toast, ToastService, ToastType } from './toast.service';

const TOAST_ICONS: Record<ToastType, string> = {
  success: 'check-circle',
  error: 'x-circle',
  warning: 'alert-triangle',
  info: 'info',
};

@Component({
  selector: 'app-toast-container',
  imports: [LucideAngularModule],
  templateUrl: './toast-container.html',
  styleUrl: './toast-container.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ToastContainerComponent {
  protected readonly service = inject(ToastService);
  protected readonly toasts = this.service.toasts;

  protected iconFor(type: ToastType): string {
    return TOAST_ICONS[type];
  }

  protected trackToast(_: number, toast: Toast): number {
    return toast.id;
  }
}
