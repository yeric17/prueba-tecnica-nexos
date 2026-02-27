import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, computed, inject } from '@angular/core';
import { AuthService } from '../../../modules/auth/services/auth.service';
import { UserMenu } from '../../components/user-menu/user-menu';

@Component({
  selector: 'app-header',
  imports: [CommonModule, UserMenu],
  templateUrl: './header.html',
  styleUrl: './header.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Header {
  private readonly authService = inject(AuthService);

  protected readonly user = this.authService.user;

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
