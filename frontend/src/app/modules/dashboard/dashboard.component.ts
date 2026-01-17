import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { AuthService } from '@core/services/auth.service';
import { User, UserRole } from '@core/models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatGridListModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  currentUser: User | null = null;
  userRole: string = '';

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.currentUser = this.authService.getCurrentUser();
    if (this.currentUser?.roles && this.currentUser.roles.length > 0) {
      this.userRole = this.currentUser.roles[0];
    }
  }

  getRoleDashboardTitle(): string {
    const roleDescriptions: { [key: string]: string } = {
      [UserRole.ADMIN]: 'System Administration',
      [UserRole.DIRECTOR]: 'Executive Dashboard',
      [UserRole.REGIONAL_PROJECT_MANAGER]: 'Regional Overview',
      [UserRole.SENIOR_MANAGER]: 'Facility Dashboard',
      [UserRole.OPERATION_MANAGER]: 'Operations Dashboard',
      [UserRole.KEY_WORKER]: 'Care Activities',
      [UserRole.SOCIAL_WORKER]: 'Case Management'
    };
    return roleDescriptions[this.userRole] || 'Dashboard';
  }
}
