import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guards/auth-guard.guard';
import { HotelsComponent } from './hotels/hotels.component';
import { DetailsComponent } from './hotels/details/details.component';
import { RoomDetailsComponent } from './hotels/room-details/room-details.component';
import { HotelManagementComponent } from './hotels/hotel-management/hotel-management.component';
import { ManageHotelComponent } from './hotels/hotel-management/manage-hotel/manage-hotel.component';
import { ManageRoomComponent } from './hotels/hotel-management/manage-hotel/manage-room/manage-room.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'hotels/management', component: HotelManagementComponent }, 
  { path: 'hotels/:hotelid/rooms/:roomid', component: RoomDetailsComponent },
  { path: 'hotels/:hotelid', component: DetailsComponent },
  { path: 'hotels', component: HotelsComponent },
  {path:  'hotels/management/:hotelid', component:ManageHotelComponent},
  {path:  'hotels/management/:hotelid/rooms/:roomid', component: ManageRoomComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
