import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { Member } from 'src/app/models/member';
import { User } from 'src/app/models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MemberService } from 'src/app/_services/member.service';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.scss']
})
export class PhotoEditorComponent  implements OnInit{
  @Input() member: Member| undefined;
  uploader: FileUploader | undefined;
  hasBaseDropZoneOver = false;
  baseUrl = 'http://localhost:5001/api/';
  user: User| undefined;
  constructor(private accountService: AccountService, private memberService: MemberService){
    this.accountService.currentUser.subscribe({
      next: user=>{
        if (user) this.user = user


      }
    });

  }
  ngOnInit(): void {
    this.initializedIploader();
  }
   fileOverBase(e:any){
      this.hasBaseDropZoneOver = e;
    }
    initializedIploader(){
      this.uploader = new FileUploader({
          url: this.baseUrl + 'users/add-photo',
          authToken: 'Bearer ' + this.user?.token,
          isHTML5 : true,
          allowedFileType:['image'],
          removeAfterUpload: true,
          autoUpload:false,
          maxFileSize: 10*1024*1024
      });
      this.uploader.onAfterAddingFile = (file)=>{
        file.withCredentials = false
      }
      this.uploader.onSuccessItem = (item,response,status,headers)=>{
        if (response) {
          const photo = JSON.parse(response);
          this.member?.photos.push(photo);

        }
      }
    }
    deletePhoto(photoId:number){
      this.memberService.deletePhoto(photoId).subscribe({
        next:_=>{
         if (this.member) {
           this.member.photos = this.member?.photos.filter(x=>x.id)

         }
        }
      })
    }


}
