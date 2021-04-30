import {Component, OnInit } from '@angular/core';
import {AccountService} from '../_services/account.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  model: any = {};

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  register(): any {
    return this.accountService.register(this.model).subscribe(response => {
      console.log(response);
    }, error => {
      console.error(error);
    });
  }
}
