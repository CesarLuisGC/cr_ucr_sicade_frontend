import { Component } from '@angular/core';
import { NavMenu } from 'src/app/core/models/base/navMenu.model';
import { SesionService } from 'src/app/core/services/sesion/sesion.service';

@Component({
  selector: 'app-sidebar-menu',
  templateUrl: './sidebar-menu.component.html',
  styleUrls: ['./sidebar-menu.component.scss']
})
export class SidebarMenuComponent {
  menus: NavMenu[] = [];

  constructor(private service: SesionService){
    this.menus = service.sesion.menus;
  }
}
