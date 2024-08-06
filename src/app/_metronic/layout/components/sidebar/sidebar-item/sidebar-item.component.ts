import { Component, Input } from '@angular/core';
import { NavItem } from 'src/app/core/models/base/navItem.model';

@Component({
  selector: 'app-sidebar-item',
  templateUrl: './sidebar-item.component.html',
  styleUrls: ['./sidebar-item.component.scss']
})
export class SidebarItemComponent {
  @Input() hijo: NavItem = new NavItem();
  @Input() level: number = 1;
}
