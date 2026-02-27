import { DOCUMENT } from '@angular/common';
import { ChangeDetectionStrategy, Component, effect, inject, signal, viewChild } from '@angular/core';
import { Menu, MenuContent, MenuItem, MenuTrigger } from '@angular/aria/menu';
import { OverlayModule } from '@angular/cdk/overlay';

type Theme = 'light' | 'dark';

@Component({
  selector: 'app-user-menu',
  imports: [Menu, MenuContent, MenuItem, MenuTrigger, OverlayModule],
  templateUrl: './user-menu.html',
  styleUrl: './user-menu.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserMenu {
  private readonly document = inject(DOCUMENT);
  private readonly themeStorageKey = 'preferred-theme';

  protected readonly theme = signal<Theme>(this.getInitialTheme());

  formatMenu = viewChild<Menu<string>>('formatMenu');

  constructor() {
    effect(() => {
      const currentTheme = this.theme();
      this.document.documentElement.dataset['theme'] = currentTheme;
      if (typeof window !== 'undefined' && window.localStorage) {
        window.localStorage.setItem(this.themeStorageKey, currentTheme);
      }
    });
  }

  protected toggleTheme(): void {
    this.theme.update(mode => (mode === 'light' ? 'dark' : 'light'));
  }

  private getInitialTheme(): Theme {
    if (typeof window === 'undefined') {
      return 'light';
    }

    const storedTheme = window.localStorage.getItem(this.themeStorageKey) as Theme | null;
    if (storedTheme === 'dark' || storedTheme === 'light') {
      return storedTheme;
    }

    return window.matchMedia?.('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
  }
}
