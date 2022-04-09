import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-profile-layout',
  templateUrl: './layout.component.html',
})
export class ProfileLayoutComponent implements OnInit {
  // user!: User;

  constructor() {}

  ngOnInit(): void {
    // this.auth.user().subscribe(user => (this.user = user));
  }
}
