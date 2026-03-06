import { ChangeDetectionStrategy, Component, computed, inject, signal } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../../modules/auth/services/auth.service';

export interface NavItem {
  label: string;
  route: string;
  icon: string;
  requiresAdmin?: boolean;
}

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Navbar {
  private authService = inject(AuthService);

  protected readonly isMenuOpen = signal(false);
  protected readonly user = this.authService.user;

  protected readonly isAdmin = computed(() => {
    const user = this.user();


    return user?.roles.map(r => r.toLowerCase()).includes('admin');
  });

  protected readonly allNavItems: NavItem[] = [
    { label: 'Productos', route: '/products/list', icon: 'inventory_2' },
    { label: 'Mis Pedidos', route: '/orders', icon: 'receipt_long' },
    { label: 'Administrar Productos', route: '/products/manager', icon: 'admin_panel_settings', requiresAdmin: true }
  ];

  protected readonly navItems = computed(() => {
    return this.allNavItems.filter(item => {
      if (item.requiresAdmin) {
        return this.isAdmin();
      }
      return true;
    });
  });

  protected toggleMenu(): void {
    this.isMenuOpen.update(value => !value);
  }

  protected closeMenu(): void {
    this.isMenuOpen.set(false);
  }
}
