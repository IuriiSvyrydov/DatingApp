import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
registerMode = false;
users:any;
ngOnInit(): void {
  this.getUsers();
}
constructor(private http: HttpClient){}
registerToggle(){
  this.registerMode = !this.registerMode;
}
getUsers(){
        this.http.get('http://localhost:5001/api/users')
    .subscribe({
      next:response=>this.users = response,
      error: error=>console.log(error),
      complete:()=>console.log('Request has completed')
    });
  }
  cancelRegisterMode(event: boolean){
    this.registerMode = event
  }

}
