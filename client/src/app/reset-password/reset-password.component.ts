import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {AccountService} from '../_services/account.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  model: any = {};

  constructor(private route: ActivatedRoute, private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    this.route.queryParams
      .subscribe(params => {
          this.model.email = params.email;
          this.model.token = params.token;
        }
      );
  }

  submit(): void {
    this.accountService.resetPassword(this.model).subscribe(next => {
      this.router.navigateByUrl('/');
    });
  }

}
