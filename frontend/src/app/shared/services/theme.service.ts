import { DOCUMENT } from '@angular/common';
import { effect, inject, Injectable, signal } from '@angular/core';

export type Theme = 'light' | 'dark';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  private readonly document = inject(DOCUMENT);
  private readonly themeStorageKey = 'preferred-theme';

  private readonly _theme = signal<Theme>(this.getInitialTheme());
  
  public readonly theme = this._theme.asReadonly();

  constructor() {
    effect(() => {
      const currentTheme = this._theme();
      this.document.documentElement.dataset['theme'] = currentTheme;
      if (typeof window !== 'undefined' && window.localStorage) {
        window.localStorage.setItem(this.themeStorageKey, currentTheme);
      }
    });
  }

  public toggleTheme(): void {
    this._theme.update(mode => (mode === 'light' ? 'dark' : 'light'));
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
