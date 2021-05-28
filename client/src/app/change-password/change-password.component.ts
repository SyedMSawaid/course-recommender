import { Component, OnInit } from '@angular/core';
import {AccountService} from '../_services/account.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  model: any = {};

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
  }

  skadoosh(): void {
   this.accountService.changePassword(this.model.oldPassword, this.model.newPassword).subscribe(
     next => {
       console.log(next);
     }
   );
   this.router.navigateByUrl('/profile');
  }
}
