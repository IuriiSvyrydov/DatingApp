import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Member } from '../models/member';
import {  map, of} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class MemberService {

  apiUrl = 'api/';
  members: Member[] = [];
  constructor(private http:HttpClient) { }

  getMembers() {
    if (this.members.length>0) return of(this.members);

    return this.http.get<Member[]>(this.apiUrl + 'users').pipe(
      map(members=>{
        this.members = members
        return members
      })
    );
  }
  getMember(username: string){
    const member = this.members.find(x=>x.userName===username);
    if (member) return of(member);




    return this.http.get<Member>(this.apiUrl + 'users/'+ username);
  }
  // getHttpOptions(){
  //   const userString = localStorage.getItem('user');
  //   if (!userString )return;
  //   const user = JSON.parse(userString);
  //   return {
  //     headers: new HttpHeaders({
  //       Autorization: 'Bearer ' + user.token
  //     })
  //   }
  // }
  updateMember(member: Member){
    return this.http.put(this.apiUrl+ 'users', member).pipe(
      map(()=>{
        const index = this.members.indexOf(member);
        this.members[index] = {...this.members[index],...member}
      })
    )
  }
  deletePhoto(photoId:number){
    return this.http.delete(this.apiUrl + 'users/delete-photo/' + photoId);
  }
}
