import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { ContainerComponent, ShadowOnScrollDirective, SidebarBrandComponent, SidebarComponent, SidebarFooterComponent, SidebarHeaderComponent, SidebarNavComponent, SidebarToggleDirective, SidebarTogglerDirective } from '@coreui/angular';
import { IconDirective } from '@coreui/icons-angular';
import { NgScrollbar } from 'ngx-scrollbar';
import { parentNavItems } from '../parent_nav';

function isOverflown(element: HTMLElement) {
  return (
    element.scrollHeight > element.clientHeight ||
    element.scrollWidth > element.clientWidth
  );
}

@Component({
  selector: 'app-parent',
  imports: [
    SidebarComponent,
    SidebarHeaderComponent,
    SidebarBrandComponent,
    RouterLink,
    IconDirective,
    NgScrollbar,
    SidebarNavComponent,
    ContainerComponent,
    RouterOutlet,
  ],
  templateUrl: './parent.html',
  styleUrl: './parent.scss',
})
export class Parent {
  public navItems = parentNavItems;
  
    onScrollbarUpdate($event: any) {
      // if ($event.verticalUsed) {
      // console.log('verticalUsed', $event.verticalUsed);
      // }
    }
}
