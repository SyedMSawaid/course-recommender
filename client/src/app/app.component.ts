import {Component, OnInit} from '@angular/core';
import {AccountService} from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Recommendation System for Academia';

  constructor(private accountService: AccountService) {
  }

  ngOnInit(): void {
    this.accountService.currentUserSource.next(null);
  }
}
