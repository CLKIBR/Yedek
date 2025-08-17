import { Injectable } from "@angular/core";
import { CanActivate, UrlTree, Router } from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "../../../core/services/auth.service";

@Injectable({ providedIn: "root" })
export class TeacherStudentLinkGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private router: Router,
  ) {}

  canActivate():
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    if (this.authService.isLoggedIn()) {
      // Add additional teacher-student link checks if needed
      return true;
    }
    return this.router.createUrlTree(["/login"]);
  }
}
