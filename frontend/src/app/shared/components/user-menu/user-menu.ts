import { ChangeDetectionStrategy, Component, inject, viewChild } from '@angular/core';
import { Menu, MenuContent, MenuItem, MenuTrigger } from '@angular/aria/menu';
import { OverlayModule } from '@angular/cdk/overlay';
import { AuthService } from '../../../modules/auth/services/auth.service';
import { ThemeService } from '../../services/theme.service';
import { LucideAngularModule } from 'lucide-angular';

@Component({
  selector: 'app-user-menu',
  imports: [Menu, MenuContent, MenuItem, MenuTrigger, OverlayModule, LucideAngularModule],
  templateUrl: './user-menu.html',
  styleUrl: './user-menu.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserMenu {
  private readonly authService = inject(AuthService);
  private readonly themeService = inject(ThemeService);

  protected readonly theme = this.themeService.theme;

  formatMenu = viewChild<Menu<string>>('formatMenu');

  protected toggleTheme(): void {
    this.themeService.toggleTheme();
  }

  protected logout(): void {
    this.authService.logout();
  }
}
