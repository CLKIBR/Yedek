import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { studentNavItems } from '../student_nav';
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
  selector: 'app-student',
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
  templateUrl: './student.html',
  styleUrl: './student.scss'
})
export class Student {
  public navItems = studentNavItems;
  
    onScrollbarUpdate($event: any) {
      // if ($event.verticalUsed) {
      // console.log('verticalUsed', $event.verticalUsed);
      // }
    }
}
