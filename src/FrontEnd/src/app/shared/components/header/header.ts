import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthFacade } from '../../../core/auth/application/auth.facade';


@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.html',
  styleUrls: ['./header.css']
})
export class Header {
  private authFacade = inject(AuthFacade);
  isDropdownOpen = false;

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  goToProfile() {
    this.isDropdownOpen = false;
    // Logic for profile navigation
  }

  logout() {
    this.isDropdownOpen = false;
    this.authFacade.logout(); 
  }
}