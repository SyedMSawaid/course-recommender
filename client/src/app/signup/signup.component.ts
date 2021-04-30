import {Component, OnInit } from '@angular/core';
import {AccountService} from '../_services/account.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  model: any = {};

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
  }

  register(): any {
    return this.accountService.register(this.model).subscribe(response => {
      console.log(response);
      this.router.navigateByUrl('/dashboard');
    }, error => {
      console.error(error);
    });
  }
}
