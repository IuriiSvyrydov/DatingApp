import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { Member } from 'src/app/models/member';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.scss']

})
export class MemberCardComponent implements OnInit {

  @Input() member: Member| undefined
  constructor(){}

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

}