import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { teacherNavItems } from '../teacher_nav';
import { ContainerComponent, ShadowOnScrollDirective, SidebarBrandComponent, SidebarComponent, SidebarFooterComponent, SidebarHeaderComponent, SidebarNavComponent, SidebarToggleDirective, SidebarTogglerDirective } from '@coreui/angular';
import { IconDirective } from '@coreui/icons-angular';
import { NgScrollbar } from 'ngx-scrollbar';

function isOverflown(element: HTMLElement) {
  return (
    element.scrollHeight > element.clientHeight ||
    element.scrollWidth > element.clientWidth
  );
}

@Component({
  selector: 'app-teacher',
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
  templateUrl: './teacher.html',
  styleUrl: './teacher.scss'
})
export class Teacher {
    public navItems = teacherNavItems;
  
    onScrollbarUpdate($event: any) {
      // if ($event.verticalUsed) {
      // console.log('verticalUsed', $event.verticalUsed);
      // }
    }
}
