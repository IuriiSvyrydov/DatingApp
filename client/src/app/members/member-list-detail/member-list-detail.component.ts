import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation } from '@kolkov/ngx-gallery';
import { NgxGalleryImage } from '@kolkov/ngx-gallery';
import {  NgxGalleryOptions} from "@kolkov/ngx-gallery";
import { Member } from 'src/app/models/member';
import { MemberService } from 'src/app/_services/member.service';

@Component({
  selector: 'app-member-list-detail',
  templateUrl: './member-list-detail.component.html',
  styleUrls: ['./member-list-detail.component.scss']
})
export class MemberListDetailComponent implements OnInit {
  member: Member| undefined;
  galleryOptions: NgxGalleryOptions[] = [];
  galleryImages: NgxGalleryImage[]=[];


  constructor(private memberService: MemberService,private router: ActivatedRoute){}
  ngOnInit(): void {
    this.loadMember();
    this.galleryOptions = [{
      width: '500px',
      height:'500px',
      imagePercent:100,
      thumbnailsColumns:4,
      imageAnimation: NgxGalleryAnimation.Slide,
      preview:false
    }]
  }
  getImages(){
    if(!this.member) return[];
    const imageUrls = [];

    for (const photo of this.member.photos) {
        imageUrls.push({
          small: photo.url,
          medium: photo.url,
          big: photo.url
        })
    }
    return imageUrls;

  }

  loadMember(){
    const username = this.router.snapshot.paramMap.get('username');
    if (!username)return;
    this.memberService.getMember(username).subscribe({
      next: member=>{
        this.member = member;
        this.galleryImages = this.getImages();


      }
    });
  }
}

