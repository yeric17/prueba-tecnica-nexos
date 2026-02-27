import { ChangeDetectionStrategy, Component, signal } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

export interface NavItem {
  label: string;
  route: string;
  icon: string;
}

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Navbar {
  protected readonly isMenuOpen = signal(false);

  protected readonly navItems: NavItem[] = [
    { label: 'Productos', route: '/products/list', icon: 'inventory_2' },
    { label: 'Mis Pedidos', route: '/orders', icon: 'receipt_long' }
  ];

  protected toggleMenu(): void {
    this.isMenuOpen.update(value => !value);
  }

  protected closeMenu(): void {
    this.isMenuOpen.set(false);
  }
}
