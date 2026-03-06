import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, computed, inject } from '@angular/core';
import { AuthService } from '../../../modules/auth/services/auth.service';
import { UserMenu } from '../../components/user-menu/user-menu';
import { UserCart } from '../../components/user-cart/user-cart';
import { Navbar } from '../../components/navbar/navbar';
import { ThemeService } from '../../../shared/services/theme.service';

@Component({
  selector: 'app-header',
  imports: [CommonModule, UserMenu, UserCart, Navbar],
  templateUrl: './header.html',
  styleUrl: './header.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Header {
  private readonly authService = inject(AuthService);
  private readonly themeService = inject(ThemeService);

  protected readonly user = this.authService.user;

  protected readonly logoColor1 = computed(() => {
    return this.themeService.theme() === 'dark' ? '#FFFFFF' : '#FF4D00';
  });

  protected readonly logoColor2 = computed(() => {
    return this.themeService.theme() === 'dark' ? '#CCCCCC' : '#391300';
  });

  protected readonly displayName = computed(() => {
    const user = this.user();
    return user?.userName ?? user?.email ?? 'Invitado';
  });

  protected readonly userInitials = computed(() => {
    const user = this.user();
    if (!user) {
      return 'U';
    }
    const initials = `${user.userName?.charAt(0) ?? ''}${user.userName?.charAt(1) ?? ''}`.trim();
    if (initials) {
      return initials.toUpperCase();
    }
    return user.email?.charAt(0).toUpperCase() ?? 'U';
  });

  protected readonly userEmail = computed(() => this.user()?.email ?? 'usuario@correo.com');

}
