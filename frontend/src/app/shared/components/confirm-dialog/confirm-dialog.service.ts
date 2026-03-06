import { Injectable, signal } from '@angular/core';

export interface ConfirmDialogConfig {
  message: string;
  title?: string;
  confirmLabel?: string;
  cancelLabel?: string;
}

interface DialogState {
  config: Required<ConfirmDialogConfig>;
  resolve: (result: boolean) => void;
}

@Injectable({ providedIn: 'root' })
export class ConfirmDialogService {
  private readonly _state = signal<DialogState | null>(null);
  readonly state = this._state.asReadonly();

  confirm(message: string, title?: string): Promise<boolean>;
  confirm(config: ConfirmDialogConfig): Promise<boolean>;
  confirm(messageOrConfig: string | ConfirmDialogConfig, title?: string): Promise<boolean> {
    const raw: ConfirmDialogConfig =
      typeof messageOrConfig === 'string' ? { message: messageOrConfig, title } : messageOrConfig;

    const config: Required<ConfirmDialogConfig> = {
      message: raw.message,
      title: raw.title ?? 'Confirmar acción',
      confirmLabel: raw.confirmLabel ?? 'Confirmar',
      cancelLabel: raw.cancelLabel ?? 'Cancelar',
    };

    return new Promise<boolean>((resolve) => {
      this._state.set({ config, resolve });
    });
  }

  resolve(result: boolean): void {
    this._state()?.resolve(result);
    this._state.set(null);
  }
}
