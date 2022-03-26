import { Component, OnInit } from '@angular/core';
import { subscribeOn } from 'rxjs';
import { areaDto } from 'src/app/dto/area-output-dto';
import { pageResultBase } from 'src/app/dto/page-result-base';
import { AreaServiceProxy } from 'src/app/service/admin-service/area-service-proxy';

@Component({
  selector: 'app-area',
  templateUrl: './area.component.html',
  styleUrls: ['./area.component.css']
})
export class AreaComponent implements OnInit {
  listArea!: pageResultBase<areaDto>;
  pageIndex!: number;
  pageSize!: number;

  constructor(private areaService: AreaServiceProxy) { }

  ngOnInit(): void {
    this.getListArea("", 1, 10)
  }

  getListArea(keyWord: string, pageIndex: number, pageSize: number) {
    this.areaService.getList(keyWord, pageIndex, pageSize).subscribe(x => {
      this.listArea = x
      console.log("danh sach area", this.listArea)
    })
  }

  deleteArea(id: number) {
    this.areaService.delete(id).subscribe(x => {
      alert(`Delete ${x}`);
      this.getListArea("", 1, 10)
    })
  }

}
